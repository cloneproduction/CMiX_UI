using CMiX.MVVM.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;

namespace CMiX.MVVM.ViewModels
{
    public class MasterBeat : Beat
    {
        public MasterBeat() : this(period: 1000.0, multiplier: 1)
        {

        }
        public MasterBeat(double period, double multiplier)
        {
            Period = period;
            Multiplier = multiplier;

            Timer = new HighResolutionTimer((float)Period);
            Timer.Elapsed += Timer_Elapsed; ;
            Timer.Start();

            tapPeriods = new List<double>();
            tapTime = new List<double>();

            ResyncCommand = new RelayCommand(p => Resync());
            TapCommand = new RelayCommand(p => Tap());
        }

        public Stopwatch Stopwatcher{ get; set; }
        public long CurrentTimeTick { get; set; }
        public long NextTimeTick { get; set; }


        private void Timer_Elapsed(object sender, HighResolutionTimerElapsedEventArgs e)
        {
            //OnBeatTap();
            //BeatTick++;
            //if (BeatTick > 3)
            //    BeatTick = 0;
        }

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
                SetAndNotify(ref _period, value);
                OnPeriodChanged(Period);
                Notify(nameof(BPM));
                OnSendChange(this.GetModel(), this.GetMessageAddress());
                if (Period > 0 && Timer != null)
                    Timer.Interval = (float)Period;
            }
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

        public override HighResolutionTimer Timer { get; set; }

        protected override void Resync()
        {
            BeatTick = 0;
            Console.WriteLine("Resync");
            //BeatTickOnReset = 0;// = BeatTick;
            //BeatTickOnReset = BeatTick;
            //Notify(nameof(BeatTickOnReset));
            if (!Timer.IsRunning) 
                return;

            Timer.Stop();
            Timer.Start();
            OnBeatResync();
            IsReset = true;
        }

        protected override void Multiply() => Period /= 2.0;

        protected override void Divide() => Period *= 2.0;

        private void Tap()
        {
            Period = GetMasterPeriod();
            if (Period > 0)
                Timer.Interval = (float)Period;
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