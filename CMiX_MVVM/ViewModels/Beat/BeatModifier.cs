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
            BeatAnimations = new BeatAnimations(masterBeat.Period);
            BeatDisplay = new BeatDisplay(masterBeat.BeatAnimations.AnimatedDoubles[index + masterBeat.BeatAnimations.AnimatedDoubles.Count / 2]);
            ChanceToHit = new Slider(nameof(ChanceToHit), this) { Amount = 1.0 };
            Beat = masterBeat;

            masterBeat.PeriodChanged += (s, newvalue) =>
            {
                OnPeriodChanged(Period);
                Notify(nameof(Period));
                Notify(nameof(BPM));
                
                BeatAnimations.MakeCollection(Period);
                BeatDisplay.AnimatedDouble = BeatAnimations.AnimatedDoubles[index + BeatAnimations.AnimatedDoubles.Count / 2];
                Notify(nameof(BeatDisplay));
                Console.WriteLine("BeatModifier PeriodChanged");
            };

            Multiplier = 1.0;
        }

        private int index = 0;
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

        public BeatDisplay BeatDisplay { get; set; }

        private BeatAnimations _beatAnimations;
        public BeatAnimations BeatAnimations
        {
            get => _beatAnimations;
            set => SetAndNotify(ref _beatAnimations, value);
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
                OnSendChange(this.GetModel(), this.GetMessageAddress());
            }
        }

        private void SetAnimatedDouble()
        {
            BeatDisplay.AnimatedDouble = Beat.BeatAnimations.AnimatedDoubles[index + Beat.BeatAnimations.AnimatedDoubles.Count / 2 - 1];
            Notify(nameof(BeatDisplay));
        }

        protected override void Multiply()
        {
            Multiplier /= 2;
            if (Beat.GetAnimatedDoubleIndex() < Beat.BeatAnimations.AnimatedDoubles.Count - 1)
            {
                index++;
                SetAnimatedDouble();
            }
        }

        protected override void Divide()
        {
            Multiplier *= 2;
            if (Beat.GetAnimatedDoubleIndex() > 0)
            {
                index--;
                SetAnimatedDouble();
            }
        }
    }
}