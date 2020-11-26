using System;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Mediator;

namespace CMiX.MVVM.ViewModels
{
    public class AnimParameter : Sender
    {
        public AnimParameter(string name, IColleague parentSender, double defaultValue, Counter counter, MasterBeat beat, bool isEnabled = true) : base (name, parentSender)
        {
            Range = new Range(nameof(Range), this, 0.0, 1.0);
            Easing = new Easing(nameof(Easing), this);
            BeatModifier = new BeatModifier(nameof(BeatModifier), this, beat);

            Name = name;
            Counter = counter;
            Counter.CounterChangeEvent += Counter_CounterChangeEvent;
            DefaultValue = defaultValue;

            IsEnabled = isEnabled;
            SelectedModeType = ModeType.None;
        }

        public override void Receive(Message message)
        {
            this.SetViewModel(message.Obj as AnimParameterModel);
            Console.WriteLine("Received AnimParameter");
        }


        public BeatModifier BeatModifier { get; set; }
        public Easing Easing { get; set; }
        public Range Range { get; set; }
        public double DefaultValue { get; set; }
        public Counter Counter { get; set; }
        public double[] Parameters { get; set; }


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
            set => SetAndNotify(ref _animMode, value);
        }

        private void Counter_CounterChangeEvent(object sender, CounterEventArgs e)
        {
            this.Parameters = new double[e.Value];
        }

        private void SetAnimMode()
        {
            this.AnimMode = ModesFactory.CreateMode(SelectedModeType, this);
            this.OnBeatTick = AnimMode.UpdateOnBeatTick;
            this.OnUpdateParameters = AnimMode.UpdateParameters;
            this.Send(new Message(MessageCommand.UPDATE_VIEWMODEL, this.Address, this.GetModel()));
        }

        public Action<AnimParameter, double> OnBeatTick { get; set; }
        public Action<AnimParameter, double> OnUpdateParameters { get; set;}
    }
}