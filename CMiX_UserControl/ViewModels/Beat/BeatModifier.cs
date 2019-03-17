using CMiX.Models;
using CMiX.Services;
using GuiLabs.Undo;
using System;
using System.Collections.ObjectModel;
using Memento;

namespace CMiX.ViewModels
{
    [Serializable]
    public class BeatModifier : Beat
    {
        #region CONSTRUCTORS
        public BeatModifier(string layername, ObservableCollection<OSCMessenger> messengers, Beat masterBeat, ActionManager actionmanager, Mementor mementor)
        : this
        (
            actionmanager: actionmanager,
            mementor: mementor,
            masterBeat: masterBeat,
            multiplier: 1.0,
            chanceToHit: new Slider(String.Format("{0}/{1}/{2}", layername, nameof(BeatModifier), "ChanceToHit"), messengers, actionmanager, mementor),
            messengers: messengers,
            messageaddress: String.Format("{0}/{1}/", layername, nameof(BeatModifier))
        )
        { }

        public BeatModifier
            (
                ActionManager actionmanager,
                Mementor mementor,
                Beat masterBeat,
                double multiplier,
                Slider chanceToHit,
                ObservableCollection<OSCMessenger> messengers,
                string messageaddress
            )
            : base(actionmanager, messengers)
        {
            Mementor = mementor;
            MasterBeat = masterBeat ?? throw new ArgumentNullException(nameof(masterBeat));
            Multiplier = multiplier;
            ChanceToHit = chanceToHit;
            ChanceToHit.Amount = 1.0; // default value for slider
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
                Mementor.PropertyChange(this, "Multiplier");
                base.Multiplier = value;
                OnPeriodChanged(Period);
                Notify(nameof(Period));
                Notify(nameof(BPM));
                SendMessages(MessageAddress + nameof(Multiplier), Multiplier);
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