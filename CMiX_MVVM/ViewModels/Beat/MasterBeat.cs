using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Timers;
using System.Windows.Input;

namespace CMiX.MVVM.ViewModels
{
    public class MasterBeat : Beat
    {
        public MasterBeat(double period, double multiplier)
        {
            Timer = new Timer();
            Timer.Elapsed += Timer_Elapsed;
            //Timer.Interval = 1000;
            Timer.Start();
            Period = period;
            Multiplier = multiplier;
            ResyncCommand = new RelayCommand(p => Resync());
            TapCommand = new RelayCommand(p => Tap());
            tapPeriods = new List<double>();
            tapTime = new List<double>();

            BeatTaped = false;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (BeatTaped == false)
                BeatTaped = true;
            else
                BeatTaped = false;
        }

        public MasterBeat() : this (period: 0.0, multiplier: 1)
        {
        
        }

        public ICommand ResyncCommand { get; }
        public ICommand TapCommand { get; }
        public Timer Timer { get; set; }

        private bool _beatTaped;
        public bool BeatTaped
        {
            get => _beatTaped;
            set => SetAndNotify(ref _beatTaped, value);
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
                    Timer.Interval = Period / 2;
                //SendMessages(MessageAddress + nameof(Period), Period);
            }
        }

        #region METHODS
        private void Resync()
        {
            //Sender.SendMessages(MessageAddress + nameof(Resync), CurrentTime + Period);
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