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
            BeatTickCount = 0;



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

        private void Timer_Elapsed(object sender, HighResolutionTimerElapsedEventArgs e)
        {
            OnBeatTap();
            BeatTickCount++;
            if (BeatTickCount > 3)
                BeatTickCount = 0;
            //Console.WriteLine("Master BeatTickCount = " + BeatTickCount);
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

        private int _beatTickCount;
        public override int BeatTickCount
        {
            get => _beatTickCount;
            set => SetAndNotify(ref _beatTickCount, value);
        }

        public override HighResolutionTimer Timer { get; set; }


        protected override void Resync()
        {
            BeatTickCount = 0;
            if (!Timer.IsRunning) 
                return;

            Timer.Stop();
            Timer.Start();
            OnBeatResync();
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
                {
                    //double average = ;
                    tapPeriods.Add(tapTime[i] - tapTime[i - 1]);
                }
            }
            return tapPeriods.Sum() / tapPeriods.Count;
        }
    }
}