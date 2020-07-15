using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using System;
using System.Windows.Media;

namespace CMiX.MVVM.ViewModels
{
    public class BeatModifier : Beat, IBeat
    {
        public BeatModifier(Beat beat)
        {
            ChanceToHit = new Slider(nameof(ChanceToHit), this) { Amount = 1.0 };
            step = 0;
            Beat = beat;
            beat.StopWatchReset += Beat_StopWatchReset;
            Multiplier = 1.0;
            beat.PeriodChanged += (s, newvalue) =>
            {
                OnPeriodChanged(Period);
                Notify(nameof(Period));
                Notify(nameof(BPM));
            };
        }

        private void Beat_StopWatchReset(object sender, EventArgs e)
        {
            //this.ResetStopWatch();
        }

        private double _progressMax;
        public double ProgressMax
        {
            get => Period;
            set => SetAndNotify(ref _progressMax, value);
        }

        public int step { get; set; }
        public override void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            var RenderTime = e as RenderingEventArgs;

            var step = Period / RenderTime.RenderingTime.Milliseconds;
            Progress += step ;
            
            base.CompositionTarget_Rendering(sender, e);
            if (Progress >= ProgressMax)
                Progress = 0;
        }

        public BeatModifier(Beat beat, Sendable parentSendable) : this(beat)
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


        public Beat Beat { get; set; }
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
                OnSendChange(this.GetModel(), this.GetMessageAddress());
            }
        }

        protected override void Multiply() => Multiplier /= 2;

        protected override void Divide() => Multiplier *= 2;
    }
}