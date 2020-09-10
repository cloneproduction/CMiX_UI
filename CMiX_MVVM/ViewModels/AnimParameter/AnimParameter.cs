using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using System;

namespace CMiX.MVVM.ViewModels
{
    public class AnimParameter : Sendable
    {
        public AnimParameter(string name, double defaultValue, Counter counter, MasterBeat beat, bool isEnabled = true)
        {
            Range = new Range(0.0, 1.0, this);
            Easing = new Easing(this);
            BeatModifier = new BeatModifier(beat, this);
            AnimMode = ModesFactory.CreateMode(SelectedModeType, this, defaultValue, this);

            Counter = counter;
            DefaultValue = defaultValue;
            Name = name;
            IsEnabled = isEnabled;
            SelectedModeType = ModeType.None;
        }

        public AnimParameter(string name, double defaultValue, Counter counter, MasterBeat beat, bool isEnabled, Sendable parentSendable) : this(name, defaultValue, counter, beat, isEnabled)
        {
            SubscribeToEvent(parentSendable);
        }

        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {
            if (e.ParentMessageAddress + this.GetMessageAddress() == e.MessageAddress)
                this.SetViewModel(e.Model as AnimParameterModel);
            else
                OnReceiveChange(e.Model, e.MessageAddress, e.ParentMessageAddress + this.GetMessageAddress());
        }

        public override string GetMessageAddress()
        {
            return $"{this.GetType().Name}/{Name}/";
        }

        public BeatModifier BeatModifier { get; set; }
        public Easing Easing { get; set; }
        public Range Range { get; set; }
        public double DefaultValue { get; set; }
        public Counter Counter { get; set; }
        public double[] Spread { get; set; }


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
            set
            {
                SetAndNotify(ref _IsEnabled, value);
                OnSendChange(this.GetModel(), this.GetMessageAddress());
            }
        }

        private ModeType _selectedModeType;
        public ModeType SelectedModeType
        {
            get => _selectedModeType;
            set
            {
                SetAndNotify(ref _selectedModeType, value);
                SetAnimMode();
                OnSendChange(this.GetModel(), this.GetMessageAddress());
            }
        }

        private AnimMode _animMode;
        public AnimMode AnimMode
        {
            get => _animMode;
            set => SetAndNotify(ref _animMode, value);
        }

        private void SetAnimMode()
        {
            AnimMode = ModesFactory.CreateMode(SelectedModeType, this, DefaultValue, this);
            OnBeatTick = AnimMode.UpdateOnBeatTick;
            OnUpdatePeriod = AnimMode.UpdatePeriod;
        }

        public Action<double> OnBeatTick { get; set; }
        public Func<double, double> OnUpdatePeriod { get; set;}
    }
}