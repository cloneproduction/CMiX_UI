using System;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Mediator;
using CMiX.MVVM.ViewModels.Observer;

namespace CMiX.MVVM.ViewModels
{
    public class AnimParameter : Sender, IObserver
    {
        public AnimParameter(string name, IColleague parentSender, double defaultValue, MasterBeat beat) : base (name, parentSender)
        {
            Period = new double[0];
            Range = new Range(nameof(Range), this, 0.0, 1.0);
            Easing = new Easing(nameof(Easing), this);
            BeatModifier = new BeatModifier(nameof(BeatModifier), this, beat);
            DefaultValue = defaultValue;
            Parameters = new double[1] { defaultValue };
            Name = name;
            SelectedModeType = ModeType.None;
        }

        public override void Receive(Message message)
        {
            this.SetViewModel(message.Obj as AnimParameterModel);
        }

        public BeatModifier BeatModifier { get; set; }
        public Easing Easing { get; set; }
        public Range Range { get; set; }
        public Counter Counter { get; set; }
        public double[] Parameters { get; set; }
        public double[] Period { get; set; }
        public double DefaultValue { get; set; }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetAndNotify(ref _name, value);
        }

        private bool _IsEnabled;
        public bool IsEnabled
        {
            get => _IsEnabled;
            set => SetAndNotify(ref _IsEnabled, value);
        }

        private ModeType _selectedModeType;
        public ModeType SelectedModeType
        {
            get => _selectedModeType;
            set
            {
                SetAndNotify(ref _selectedModeType, value);
                SetAnimMode();
            }
        }

        private IAnimMode _animMode;
        public IAnimMode AnimMode
        {
            get => _animMode;
            set
            {
                SetAndNotify(ref _animMode, value);
                this.Send(new Message(MessageCommand.UPDATE_VIEWMODEL, this.Address, this.GetModel()));
            }
        }

        private void SetAnimMode()
        {
            ParametersToDefault();
            this.AnimMode = ModesFactory.CreateMode(SelectedModeType, this);
        }

        public void AnimateOnBeatTick()
        {
            var index = BeatModifier.BeatIndex;
            if (index >= 0 && index < Period.Length)
                this.AnimMode.UpdateOnBeatTick(this.Parameters, Period[BeatModifier.BeatIndex], this.Range, this.Easing);
        }

        public void AnimateOnGameLoop()
        {
            this.AnimMode.UpdateOnGameLoop(this.Parameters, Period[BeatModifier.BeatIndex], this.Range, this.Easing);
        }

        public void Update(int count)
        {
            this.Parameters = new double[count];
            ParametersToDefault();
            AnimateOnBeatTick();
        }

        private void ParametersToDefault()
        {
            for (int i = 0; i < Parameters.Length; i++)
            {
                this.Parameters[i] = DefaultValue;
            }
        }
    }
}