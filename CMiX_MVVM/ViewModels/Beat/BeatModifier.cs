using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using System;

namespace CMiX.MVVM.ViewModels
{
    public class BeatModifier : Beat, IBeat
    {
        public BeatModifier(MasterBeat beat)
        {
            ChanceToHit = new Slider(nameof(ChanceToHit), this) { Amount = 1.0 };
            step = 0;
            Beat = beat;

            Timer = new HighResolutionTimer((float)Period);
            Timer.Elapsed += Timer_Elapsed; ;
            Timer.Start();
            //CompositionTarget.Rendering check again

            beat.BeatTap += Beat_BeatTap;
            beat.BeatResync += Beat_BeatResync;
            beat.PeriodChanged += (s, newvalue) =>
            {
                OnPeriodChanged(Period);
                Notify(nameof(Period));
                Notify(nameof(BPM));
                if (Period > 0 && Timer != null)
                    Timer.Interval = (float)Period;
            };



            Multiplier = 1.0;
            BeatTickCount = beat.BeatTickCount;
        }

        private void Timer_Elapsed(object sender, HighResolutionTimerElapsedEventArgs e)
        {
            BeatTickCount++;
            if (BeatTickCount >= 4)
                BeatTickCount = 0;
        }

        public BeatModifier(MasterBeat beat, Sendable parentSendable) : this(beat)
        {
            SubscribeToEvent(parentSendable);
        }

        private void Beat_BeatTap(object sender, EventArgs e)
        {

        }

        protected override void Resync()
        {
            BeatTickCount = 0;
            if (!Timer.IsRunning)
                return;

            Timer.Stop();
            Timer.Start();
        }

        private void Beat_BeatResync(object sender, EventArgs e)
        {
            Resync();
        }

        public int step { get; set; }

        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {
            if (e.ParentMessageAddress + this.GetMessageAddress() == e.MessageAddress)
                this.SetViewModel(e.Model as BeatModifierModel);
            else
                OnReceiveChange(e.Model, e.MessageAddress, e.ParentMessageAddress + this.GetMessageAddress());
        }

        public MasterBeat Beat { get; set; }
        public Slider ChanceToHit { get; }

        public override HighResolutionTimer Timer { get; set; }

        private int _beatTickCount;
        public override int BeatTickCount
        {
            get => _beatTickCount; 
            set => SetAndNotify(ref _beatTickCount, value);
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
                Timer.Interval = (float)Period;
            }
        }

        protected override void Multiply() => Multiplier /= 2;

        protected override void Divide() => Multiplier *= 2;
    }
}