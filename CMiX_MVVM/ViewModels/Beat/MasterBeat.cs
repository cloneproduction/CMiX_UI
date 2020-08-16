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
            Index = 0;
            Period = period;
            Multiplier = multiplier;

            BeatAnimations = new BeatAnimations(period);
            SetAnimatedDouble();
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

        public event EventHandler IndexChanged;
        protected void OnIndexChanged() => IndexChanged?.Invoke(this, null);

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

        private BeatAnimations _beatAnimations;
        public BeatAnimations BeatAnimations
        {
            get => _beatAnimations;
            set => SetAndNotify(ref _beatAnimations, value);
        }

        private AnimatedDouble _animatedDouble;
        public AnimatedDouble AnimatedDouble
        {
            get => _animatedDouble;
            set => SetAndNotify(ref _animatedDouble, value);
        }

        public void Resync()
        {
            OnBeatResync();
            BeatAnimations.ResetAnimation();
        }

        private void SetAnimatedDouble()
        {
            AnimatedDouble = BeatAnimations.AnimatedDoubles[Index + (BeatAnimations.AnimatedDoubles.Count - 1) / 2];
        }

        protected override void Multiply()
        {
            if (Index >= maxIndex)
                return;
            Period /= 2.0;
            Index++;
            SetAnimatedDouble();
        }

        protected override void Divide()
        {
            if (Index <= minIndex)
                return;
            Period *= 2.0;
            Index--;
            SetAnimatedDouble();
        }

        private void Tap()
        {
            Period = GetMasterPeriod();
            
            BeatAnimations.MakeCollection(Period);
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
    }
}