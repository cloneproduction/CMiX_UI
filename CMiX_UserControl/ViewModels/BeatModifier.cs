using System;

namespace CMiX.ViewModels
{
    public class BeatModifier : Beat
    {
        public BeatModifier(Beat masterBeat)
            : this(
                  masterBeat: masterBeat,
                  multiplier: 1.0,
                  chanceToHit: 1.0)
        { }

        public BeatModifier(Beat masterBeat, double multiplier, double chanceToHit)
        {
            MasterBeat = masterBeat ?? throw new ArgumentNullException(nameof(masterBeat));
            Multiplier = multiplier;
            ChanceToHit = chanceToHit;

            masterBeat.PeriodChanged += (s, newValue) =>
            {
                OnPeriodChanged(Period);
                Notify(nameof(Period));
                Notify(nameof(BPM));
            };
        }

        private Beat MasterBeat { get; }

        public override double Period
        {
            get => MasterBeat.Period * Multiplier;
            set => throw new InvalidOperationException("Property is readonly. When binding, use Mode=OneWay.");
        }

        public override double Multiplier
        {
            get => base.Multiplier;
            set
            {
                base.Multiplier = value;
                OnPeriodChanged(Period);
                Notify(nameof(Period));
                Notify(nameof(BPM));
            }
        }

        private double _chanceToHit;
        public double ChanceToHit
        {
            get => _chanceToHit;
            set => SetAndNotify(ref _chanceToHit, value);
        }

        protected override void Multiply()
        {
            Multiplier /= 2;
        }

        protected override void Divide()
        {
            Multiplier *= 2;
        }
    }
}