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
            ChanceToHit = new Slider(nameof(ChanceToHit), this) { Amount = 1.0 };
            Beat = masterBeat;

            masterBeat.PeriodChanged += (s, newvalue) =>
            {
                OnPeriodChanged(Period);
                Notify(nameof(Period));
                Notify(nameof(BPM));
                Notify(nameof(SelectedAnimation));
            };

            Multiplier = 1.0;
            CurrentIndex = 0;
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
                Notify(nameof(SelectedAnimation));
                OnSendChange(this.GetModel(), this.GetMessageAddress());
            }
        }

        private AnimatedDouble _selectedAnimation;
        public AnimatedDouble SelectedAnimation
        {
            get => ((MasterBeat)this.Beat).BeatDisplay.AnimationCollection[CurrentIndex];
            set => SetAndNotify(ref _selectedAnimation, value);
        }

        private int _currentIndex;
        public int CurrentIndex
        {
            get => _currentIndex;
            set
            {
                Notify(nameof(SelectedAnimation));
                SetAndNotify(ref _currentIndex, value);
            }
        }

        protected override void Multiply()
        {
            Multiplier /= 2;
            CurrentIndex++;
        }

        protected override void Divide()
        {
            Multiplier *= 2;
            CurrentIndex--;
        }
    }
}