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
        public MasterBeat(double period, double multiplier)
        {
            Clock = new Clock();
            Clock.OnTick += Clock_OnTick;
            Period = period;
            BeatTick = 0;

            Multiplier = multiplier;
            ResyncCommand = new RelayCommand(p => Resync());
            TapCommand = new RelayCommand(p => Tap());
            tapPeriods = new List<double>();
            tapTime = new List<double>();
        }

        private void Clock_OnTick(object sender, EventArgs e)
        {
            BeatTick++;
            if (BeatTick >= 4)
                BeatTick = 0;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {

        }

        public MasterBeat() : this (period: 0.0, multiplier: 1)
        {
        
        }

        public ICommand ResyncCommand { get; }
        public ICommand TapCommand { get; }
        public Clock Clock { get; set; }
        public DispatcherTimer Timer { get; set; }
        public Stopwatch Stopwatch { get; set; }


        private int _beatTick;
        public int BeatTick
        {
            get => _beatTick;
            set => SetAndNotify(ref _beatTick, value);
        }

        private readonly List<double> tapPeriods;
        private readonly List<double> tapTime;

        private double CurrentTime => (DateTime.Now - DateTime.MinValue).TotalMilliseconds;

        private double _period;
        public override double Period
        {
            get => _period;
            set
            {
                SetAndNotify(ref _period, value);
                OnPeriodChanged(Period);
                Notify(nameof(BPM));
                if (Period > 0)
                {
                    Clock.Stop();
                    Clock.Interval = Convert.ToInt32(Period);
                    Clock.Start();
                }
                    
                //    Timer.Interval = new TimeSpan(Convert.ToInt64(Period) * Convert.ToInt64(10000));
            }
        }

        #region METHODS
        private void Resync()
        {
            BeatTick = 0;
            Clock.Stop();
            Clock.Start();
            //Timer.Stop();
            //Timer.Interval = new TimeSpan(Convert.ToInt64(Period * 10000));
            //Timer.Start();
            
        }

        protected override void Multiply()
        {
            Period /= 2.0;
        }

        protected override void Divide()
        {
            Period *= 2.0;
        }

        private void Tap()
        {
            Period = GetMasterPeriod();
            Console.WriteLine("Period " + Period);
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
                    double average = tapTime[i] - tapTime[i - 1];
                    tapPeriods.Add(average);
                }
            }
            return tapPeriods.Sum() / tapPeriods.Count;
        }
        #endregion
    }
}