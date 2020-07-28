using CMiX.MVVM.Controls;
using CMiX.MVVM.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Input;
using System.Windows.Threading;

namespace CMiX.MVVM.ViewModels
{
    public class MasterBeat : Beat
    {
        public MasterBeat() : this(period: 1000.0, multiplier: 1)
        {

        }
        public MasterBeat(double period, double multiplier)
        {
            BeatDependencyObject = new BeatDependencyObject();

            Period = period;
            Multiplier = multiplier;

            Stopwatcher = new Stopwatch();
            tapPeriods = new List<double>();
            tapTime = new List<double>();
            Timing = new Timer(TimerCallback, null, 0, Convert.ToInt32(Period));

            

            ResyncCommand = new RelayCommand(p => Resync());
            TapCommand = new RelayCommand(p => Tap());
        }

        public Timer Timing { get; set; }
        void TimerCallback(object state)
        {
            OnBeatTap();

            BeatTick++;
            if (BeatTick > 3)
                BeatTick = 0;
            //previousValue = currentValue;
        }

        public Stopwatch Stopwatcher{ get; set; }



        public MasterBeat(double period, double multiplier, Sendable parentSendable) : this(period, multiplier)
        {
            SubscribeToEvent(parentSendable);
        }

        public ICommand ResyncCommand { get; }
        public ICommand TapCommand { get; }

        private readonly List<double> tapPeriods;
        private readonly List<double> tapTime;

        private double CurrentTime => (DateTime.UtcNow - DateTime.MinValue).TotalMilliseconds;

        private double _period;
        public override double Period
        {
            get => _period;
            set
            {
                Console.WriteLine("PeriodChanged From VIEWMODEL");
                if (BeatDependencyObject != null)
                    BeatDependencyObject.Period = value;
                SetAndNotify(ref _period, value);
                OnPeriodChanged(Period);
                Notify(nameof(BPM));
                OnSendChange(this.GetModel(), this.GetMessageAddress());
            }
        }

        private BeatDependencyObject _beatDependencyObject;
        public BeatDependencyObject BeatDependencyObject
        {
            get => _beatDependencyObject;
            set => SetAndNotify(ref _beatDependencyObject, value);
        }


        private bool _isReset;
        public bool IsReset
        {
            get => _isReset;
            set => SetAndNotify(ref _isReset, value);
        }

        private int _beatTick;
        public int BeatTick
        {
            get => _beatTick;
            set => SetAndNotify(ref _beatTick, value);
        }

        private int _beatTickOnReset;
        public int BeatTickOnReset
        {
            get => _beatTickOnReset;
            set => SetAndNotify(ref _beatTickOnReset, value);
        }

        public void Resync()
        {
            OnBeatResync();
            IsReset = true;
            BeatDependencyObject.Resync();
        }

        protected override void Multiply() => Period /= 2.0;

        protected override void Divide() => Period *= 2.0;

        private void Tap()
        {
            Period = GetMasterPeriod();
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
    }
}