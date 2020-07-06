using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;

namespace CMiX.MVVM.ViewModels
{
    public class MasterBeat : Beat
    {
        public MasterBeat() : this(period: 0.0, multiplier: 1)
        {

        }
        public MasterBeat(double period, double multiplier)
        {
            Period = period;
            Multiplier = multiplier;
            BeatTick = 0;

            

           
            tapPeriods = new List<double>();
            tapTime = new List<double>();

            ResyncCommand = new RelayCommand(p => Resync());
            TapCommand = new RelayCommand(p => Tap());
        }

        public MasterBeat(double period, double multiplier, Sendable parentSendable) : this(period, multiplier)
        {
            SubscribeToEvent(parentSendable);
        }

        public void Reset()
        {
            Stopwatch.Reset();
            Stopwatch.Start();
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
                if (Period > 0)
                    BeatTick = 0;
                OnSendChange(this.GetModel(), this.GetMessageAddress());
            }
        }

        private void Resync()
        {
            BeatTick = 0;
            Reset();
        }

        protected override void Multiply() => Period /= 2.0;

        protected override void Divide() => Period *= 2.0;

        private void Tap() => Period = GetMasterPeriod();


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