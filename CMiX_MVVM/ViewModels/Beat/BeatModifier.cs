using System;
using CMiX.MVVM.Controls;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public class BeatModifier : Beat
    {
        public BeatModifier(MasterBeat masterBeat)
        {
            Index = 0;
            BeatIndex = 2;
            ChanceToHit = new Slider(nameof(ChanceToHit), this) { Amount = 100.0 };
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
                //BeatIndex = (Beat.Index + (Beat.BeatAnimations.AnimatedDoubles.Count - 1) / 2) + Index;
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
        public BeatAnimations BeatAnimations { get; set; }


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

        private int _beatIndex;
        public int BeatIndex
        {
            get => _beatIndex;
            set => _beatIndex = value;
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
                BeatIndex = (Beat.Index + (Beat.BeatAnimations.AnimatedDoubles.Count - 1) / 2) + Index;
                Console.WriteLine(BeatIndex + " BeatIndex");
                OnSendChange(this.GetModel(), this.GetMessageAddress());
            }
        }

        public AnimatedDouble AnimatedDouble
        {
            get => Beat.BeatAnimations.AnimatedDoubles[(Beat.Index + (Beat.BeatAnimations.AnimatedDoubles.Count - 1) / 2) + Index];
            set => throw new InvalidOperationException("Property is readonly. When binding, use Mode=OneWay.");
        }

        protected override void Multiply()
        {
            if (Index < maxIndex)
            {
                Index++;
                Multiplier /= 2.0;
            }
        }

        protected override void Divide()
        {
            if (Index > minIndex)
            {
                Index--;
                Multiplier *= 2.0;
            }
        }
    }
}