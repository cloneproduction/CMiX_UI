using CMiX.Models;
using CMiX.Services;
using System;
using System.Diagnostics;

namespace CMiX.ViewModels
{
    [Serializable]
    public class BeatModifier : Beat, IMessengerData
    {
        public BeatModifier( string layername, IMessenger messenger, Beat masterBeat)
            : this(
                  masterBeat: masterBeat,
                  multiplier: 1.0,
                  chanceToHit: 1.0,

                  messenger: messenger,
                  messageaddress: String.Format("{0}/{1}/", layername, nameof(BeatModifier)),
                  messageEnabled: true)
        { }

        public BeatModifier(
            Beat masterBeat, 
            double multiplier, 
            double chanceToHit,

            IMessenger messenger,
            string messageaddress,
            bool messageEnabled)
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

            Messenger = messenger ?? throw new ArgumentNullException(nameof(Messenger));
            MessageAddress = messageaddress;
            MessageEnabled = messageEnabled;
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

        [OSC]
        public override double Multiplier
        {
            get => base.Multiplier;
            set
            {
                base.Multiplier = value;
                OnPeriodChanged(Period);
                Notify(nameof(Period));
                Notify(nameof(BPM));
                if (MessageEnabled)
                    Messenger.SendMessage(MessageAddress + nameof(Multiplier), Multiplier);
            }
        }

        private double _chanceToHit;
        [OSC]
        public double ChanceToHit
        {
            get => _chanceToHit;
            set
            {
                SetAndNotify(ref _chanceToHit, value);
                if(MessageEnabled)
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

        public void Copy(BeatModifierDTO beatmodifierdto)
        {
            beatmodifierdto.ChanceToHit = ChanceToHit;
            beatmodifierdto.Multiplier = Multiplier;
        }

        public void Paste(BeatModifierDTO beatmodifierdto)
        {
            MessageEnabled = false;

            ChanceToHit = beatmodifierdto.ChanceToHit;
            Multiplier = beatmodifierdto.Multiplier;

            MessageEnabled = true;
        }
    }
}