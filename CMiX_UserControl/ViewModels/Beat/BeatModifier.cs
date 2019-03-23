using CMiX.Models;
using CMiX.Services;
using System;
using System.Collections.ObjectModel;
using Memento;

namespace CMiX.ViewModels
{
    [Serializable]
    public class BeatModifier : Beat
    {
        #region CONSTRUCTORS
        public BeatModifier(string layername, ObservableCollection<OSCMessenger> messengers, Beat masterBeat, Mementor mementor)
        : this
        (
            mementor: mementor,
            messengers: messengers,
            messageaddress: String.Format("{0}/{1}/", layername, nameof(BeatModifier)),
            masterBeat: masterBeat,
            multiplier: 1.0,
            chanceToHit: new Slider(String.Format("{0}/{1}/{2}", layername, nameof(BeatModifier), "ChanceToHit"), messengers, mementor)
        )
        { }

        public BeatModifier
            (
                Mementor mementor,
                ObservableCollection<OSCMessenger> messengers,
                Beat masterBeat,
                double multiplier,
                Slider chanceToHit,
                string messageaddress
            )
            : base(messengers)
        {
            Mementor = mementor;
            MasterBeat = masterBeat ?? throw new ArgumentNullException(nameof(masterBeat));
            Multiplier = multiplier;
            ChanceToHit = chanceToHit;
            ChanceToHit.Amount = 1.0;
            masterBeat.PeriodChanged += (s, newValue) =>
            {
                OnPeriodChanged(Period);
                Notify(nameof(Period));
                Notify(nameof(BPM));
            };

            Messengers = messengers ?? throw new ArgumentNullException(nameof(Messengers));
            MessageAddress = messageaddress;
        }
        #endregion

        #region PROPERTIES
        private Beat MasterBeat { get; }
        public Slider ChanceToHit { get; }

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
                SendMessages(MessageAddress + nameof(Multiplier), Multiplier);
                Mementor.PropertyChange(this, "Multiplier");
            }
        }
        #endregion

        #region MULTIPLY/DIVIDE
        protected override void Multiply()
        {
            Multiplier /= 2;
        }

        protected override void Divide()
        {
            Multiplier *= 2;
        }
        #endregion

        #region COPY/PASTE
        public void Copy(BeatModifierDTO beatmodifierdto)
        {
            ChanceToHit.Copy(beatmodifierdto.ChanceToHit);
            beatmodifierdto.Multiplier = Multiplier;
        }

        public void Paste(BeatModifierDTO beatmodifierdto)
        {
            DisabledMessages();

            ChanceToHit.Paste(beatmodifierdto.ChanceToHit);
            Multiplier = beatmodifierdto.Multiplier;

            EnabledMessages();
        }
        #endregion
    }
}