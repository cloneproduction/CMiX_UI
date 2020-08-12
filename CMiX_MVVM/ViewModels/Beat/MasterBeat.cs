using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using CMiX.MVVM.Controls;

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

            BeatAnimations = new BeatAnimations(period);
            BeatDisplay = new BeatDisplay(BeatAnimations.AnimatedDoubles[index + BeatAnimations.AnimatedDoubles.Count / 2 - 1]);

            tapPeriods = new List<double>();
            tapTime = new List<double>();

            ResyncCommand = new RelayCommand(p => Resync());
            TapCommand = new RelayCommand(p => Tap());
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
        private int index = 0;

        public override double Period
        {
            get => _period;
            set
            {
                SetAndNotify(ref _period, value);
                OnPeriodChanged(Period);
                Notify(nameof(BPM));
                OnSendChange(this.GetModel(), this.GetMessageAddress());
            }
        }

        private BeatDisplay _beatDisplay;
        public BeatDisplay BeatDisplay
        {
            get => _beatDisplay;
            set => SetAndNotify(ref _beatDisplay, value);
        }

        private BeatAnimations _beatAnimations;
        public BeatAnimations BeatAnimations
        {
            get => _beatAnimations;
            set => SetAndNotify(ref _beatAnimations, value);
        }

        public void Resync()
        {
            OnBeatResync();
            BeatAnimations.ResetAnimation();
        }

        public int GetAnimatedDoubleIndex()
        {
            return index + BeatAnimations.AnimatedDoubles.Count / 2;
        }

        private void SetAnimatedDouble()
        {
            BeatDisplay.AnimatedDouble = BeatAnimations.AnimatedDoubles[index + BeatAnimations.AnimatedDoubles.Count / 2];
        }

        protected override void Multiply()
        {
            if(GetAnimatedDoubleIndex() < BeatAnimations.AnimatedDoubles.Count - 1)
            {
                Period /= 2.0;
                index++;
                BeatAnimations.MakeCollection(Period);
                SetAnimatedDouble();
            }
        }

        protected override void Divide()
        {
            if (GetAnimatedDoubleIndex() > 0)
            {
                Period *= 2.0;
                index--;
                BeatAnimations.MakeCollection(Period);
                SetAnimatedDouble();
            }
        }

        private void Tap()
        {
            Period = GetMasterPeriod();
            BeatAnimations.MakeCollection(Period);
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
    }
}