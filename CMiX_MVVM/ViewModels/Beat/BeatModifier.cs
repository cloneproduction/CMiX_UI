using System;
using CMiX.MVVM.Controls;
using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public class BeatModifier : Beat, IBeat
    {
        public BeatModifier(MasterBeat masterBeat)
        {
            Index = 0;
            ChanceToHit = new Slider(nameof(ChanceToHit), this) { Amount = 1.0 };
            Beat = masterBeat;
            Multiplier = 1.0;

            masterBeat.IndexChanged += (s, newvalue) =>
            {
                Notify(nameof(AnimatedDouble));
            };

            masterBeat.PeriodChanged += (s, newvalue) =>
            {
                OnPeriodChanged(Period);
                Notify(nameof(Period));
                Notify(nameof(BPM));
            };
        }

        public BeatModifier(MasterBeat beat, Sendable parentSendable) : this(beat)
        {
            SubscribeToEvent(parentSendable);
        }

        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {
            if (e.ParentMessageAddress + this.GetMessageAddress() == e.MessageAddress)
                this.SetViewModel(e.Model as BeatModifierModel);
            else
                OnReceiveChange(e.Model, e.MessageAddress, e.ParentMessageAddress + this.GetMessageAddress());
        }

        public MasterBeat Beat { get; set; }
        public Slider ChanceToHit { get; }

        private BeatAnimations _beatAnimations;
        public BeatAnimations BeatAnimations
        {
            get => _beatAnimations;
            set => SetAndNotify(ref _beatAnimations, value);
        }

        private int maxIndex = 4;
        private int minIndex = -4;

        private int _index;
        public int Index
        {
            get => _index;
            set => _index = value;
        }

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
                Notify(nameof(AnimatedDouble));
                OnSendChange(this.GetModel(), this.GetMessageAddress());
            }
        }

        public AnimatedDouble AnimatedDouble
        {
            get
            {
                return Beat.BeatAnimations.AnimatedDoubles[(Beat.Index + (Beat.BeatAnimations.AnimatedDoubles.Count - 1) / 2) + Index];
            }
            set => throw new InvalidOperationException("Property is readonly. When binding, use Mode=OneWay.");
        }

        protected override void Multiply()
        {
            if (Index < maxIndex)
            {
                Index++;
                Multiplier /= 2.0;
                Notify(nameof(AnimatedDouble));
            }
        }

        protected override void Divide()
        {
            if (Index > minIndex)
            {
                Index--;
                Multiplier *= 2.0;
                Notify(nameof(AnimatedDouble));
            }
        }
    }
}