using CMiX.Services;
using System;

namespace CMiX.ViewModels
{
    public class BeatModifier : Beat, IMessengerData
    {
        public BeatModifier( string layername, IMessenger messenger, Beat masterBeat)
            : this(
                  messenger: messenger,
                  messageaddress: String.Format("{0}/{1}/", layername, nameof(BeatModifier)),
                  masterBeat: masterBeat,
                  multiplier: 1.0,
                  chanceToHit: 1.0)
        { }

        public BeatModifier(
            IMessenger messenger,
            string messageaddress,
            Beat masterBeat, 
            double multiplier, 
            double chanceToHit)
        {
            Messenger = messenger ?? throw new ArgumentNullException(nameof(Messenger));
            MessageAddress = messageaddress;
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

        private IMessenger Messenger { get; }

        public string MessageAddress { get; set; }

        public bool MessageEnabled { get; set; }

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
                Messenger.SendMessage(MessageAddress + nameof(Multiplier), Multiplier);
            }
        }

        private double _chanceToHit;
        public double ChanceToHit
        {
            get => _chanceToHit;
            set
            {
                SetAndNotify(ref _chanceToHit, value);
                Messenger.SendMessage(MessageAddress + nameof(ChanceToHit), ChanceToHit);
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