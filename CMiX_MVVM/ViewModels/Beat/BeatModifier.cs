using System;

namespace CMiX.MVVM.ViewModels
{
    public class BeatModifier : Beat
    {
        public BeatModifier(Beat beat)
        {
            Beat = beat;
            Multiplier = 1.0;
            ChanceToHit = new Slider(nameof(ChanceToHit))
            {
                Amount = 1.0
            };
            beat.PeriodChanged += (s, newvalue) =>
            {
                OnPeriodChanged(Period);
                Notify(nameof(Period));
                Notify(nameof(BPM));
            };
        }

        private Beat Beat { get; }
        public Slider ChanceToHit { get; }

        public override double Period
        {
            get => Beat.Period * Multiplier;
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
                //SendMessages(MessageAddress + nameof(Multiplier), Multiplier);
            }
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