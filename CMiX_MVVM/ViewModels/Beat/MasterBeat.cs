using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using CMiX.MVVM.Controls;
using CMiX.MVVM.Models.Beat;
using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public class MasterBeat : Beat
    {
        public MasterBeat()
        {
            Index = 0;
            Period = 1000;
            Multiplier = 1;
            Periods = new double[15];

            BeatAnimations = new BeatAnimations();
            Resync = new Resync(BeatAnimations, this);

            UpdatePeriods(Period);
            SetAnimatedDouble();

            tapPeriods = new List<double>();
            tapTime = new List<double>();
            TapCommand = new RelayCommand(p => Tap());
        }

        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {
            if (e.ParentMessageAddress + this.GetMessageAddress() == e.MessageAddress)
                this.SetViewModel(e.Model as MasterBeatModel);
            else
                OnReceiveChange(e.Model, e.MessageAddress, e.ParentMessageAddress + this.GetMessageAddress());
        }

        public MasterBeat(Sender parentSender) : this()
        {
            SubscribeToEvent(parentSender);
        }

        public ICommand ResyncCommand { get; }
        public ICommand TapCommand { get; }

        private readonly List<double> tapPeriods;
        private readonly List<double> tapTime;

        private double CurrentTime => (DateTime.UtcNow - DateTime.MinValue).TotalMilliseconds;
        

        private int maxIndex = 3;
        private int minIndex = -3;

        private int _index;
        public int Index
        {
            get => _index;
            set
            {
                _index = value;
                OnIndexChanged();
            }
        }

        private int _beatIndex;
        public int BeatIndex
        {
            get => _beatIndex;
            set => _beatIndex = value;
        }


        public event EventHandler IndexChanged;
        protected void OnIndexChanged() => IndexChanged?.Invoke(this, null);


        private double _period;
        public override double Period
        {
            get => _period;
            set => SetAndNotify(ref _period, value);
        }


        private double[] _periods;
        public double[] Periods
        {
            get => _periods;
            set => SetAndNotify(ref _periods, value);
        }


        public BeatAnimations BeatAnimations { get; set; }
        public Resync Resync { get; set; }

        private AnimatedDouble _animatedDouble;
        public AnimatedDouble AnimatedDouble
        {
            get => _animatedDouble;
            set => SetAndNotify(ref _animatedDouble, value);
        }

        private void SetAnimatedDouble()
        {
            BeatIndex = Index + (Periods.Length - 1) / 2;
            Period = Periods[Index + (Periods.Length - 1) / 2];
            AnimatedDouble = BeatAnimations.AnimatedDoubles[Index + (Periods.Length - 1) / 2];
            OnPeriodChanged(Period);
            Notify(nameof(BPM));
            OnSendChange(this.GetModel(), this.GetMessageAddress());
        }

        protected override void Multiply()
        {
            if (Index <= minIndex)
                return;
            Index--;
            SetAnimatedDouble();
        }

        protected override void Divide()
        {
            if (Index >= maxIndex)
                return;
            Index++;
            SetAnimatedDouble();
        }

        private void Tap()
        {
            UpdatePeriods(GetMasterPeriod());
            Index = 0;
            SetAnimatedDouble();
        }


        private double GetMasterPeriod()
        {
            double ms = CurrentTime;

            if (tapTime.Count > 1 && ms - tapTime[tapTime.Count - 1] > 5000)
                tapTime.Clear();

            tapTime.Add(ms);

            if (tapTime.Count > 1)
            {
                tapPeriods.Clear();
                for (int i = 1; i < tapTime.Count; i++)
                    tapPeriods.Add(tapTime[i] - tapTime[i - 1]);
            }
            return tapPeriods.Sum() / tapPeriods.Count;
        }


        private void UpdatePeriods(double period)
        {
            Period = period;
            if (period > 0)
            {
                double Multiplier = 1.0 / 128.0;
                for (int i = 0; i < Periods.Length; i++)
                {
                    Periods[i] = Multiplier * Period;
                    Multiplier *= 2;
                }
                BeatAnimations.MakeStoryBoard(Periods);
            }
        }
    }
}