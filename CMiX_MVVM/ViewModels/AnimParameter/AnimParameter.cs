using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.Resources;
using CMiX.MVVM.Services;
using System;

namespace CMiX.MVVM.ViewModels
{
    public class AnimParameter : Sendable
    {
        public AnimParameter(string name, MasterBeat beat, bool isEnabled = true)
        {
            Range = new Range(0.0, 1.0, this);
            Easing = new Easing(this);

            BeatModifier = new BeatModifier(beat, this);
            SelectedModeType = ModeType.None;
            Name = name;
            IsEnabled = isEnabled;
        }

        public AnimParameter(string name, MasterBeat beat, bool isEnabled, Sendable parentSendable) : this(name, beat, isEnabled)
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

        //private IAnimMode _animMode;
        //public IAnimMode AnimMode
        //{
        //    get => _animMode;
        //    set
        //    {
        //        SetAndNotify(ref _animMode, value);
        //        OnSendChange(this.GetModel(), this.GetMessageAddress());
        //    }
        //}

       

        private void SetAnimMode()
        {
            Console.WriteLine("SetAnimMode");
            if (SelectedModeType == ModeType.LFO)
            {
                Update = LFO;
                Console.WriteLine("LFO");
            }
               
            else if (SelectedModeType == ModeType.Steady)
            {
                Update = Steady;
                Console.WriteLine("Steady");
            }
                
            else if (SelectedModeType == ModeType.None)
            {
                Update = None;
                Console.WriteLine("None");
            }
                
            //AnimMode = ModesFactory.CreateMode(SelectedModeType, this);
        }

        private double map(double value, double fromLow, double fromHigh, double toLow, double toHigh)
        {
            return (value - fromLow) * (toHigh - toLow) / (fromHigh - fromLow) + toLow;
        }

        public Func<double, double> Update { get; set; }

        public double None(double period)
        {
            return 1.0;
        }
        public double Steady(double period)
        {
            return 100.0;
        }

        public double LFO(double period)
        {
            return Utils.Map(Easings.Interpolate((float)period, Easing.SelectedEasing), 0.0, 1.0, Range.Minimum, Range.Maximum);
        }

        public double Random(double period)
        {
            return 155.0;
        }
    }
}
