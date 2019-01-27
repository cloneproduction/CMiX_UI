using CMiX.Models;
using CMiX.Services;
using GuiLabs.Undo;
using System;

namespace CMiX.ViewModels
{
    [Serializable]
    public class BeatModifier : Beat
    {
        #region CONSTRUCTORS
        public BeatModifier(string layername, OSCMessenger messenger, Beat masterBeat, ActionManager actionmanager)
        : this
        (
            actionmanager: actionmanager,
            masterBeat: masterBeat,
            multiplier: 1.0,
            chanceToHit: new Slider(String.Format("{0}/{1}/{2}", layername, nameof(BeatModifier), "ChanceToHit"), messenger, actionmanager),
            messenger: messenger,
            messageaddress: String.Format("{0}/{1}/", layername, nameof(BeatModifier))
        )
        { }

        public BeatModifier
            (
                ActionManager actionmanager,
                Beat masterBeat,
                double multiplier,
                Slider chanceToHit,
                OSCMessenger messenger,
                string messageaddress
            )
            : base(actionmanager, messenger)
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
        }
        #endregion

        #region PROPERTIES
        public string MessageAddress { get; set; }
        public bool MessageEnabled { get; set; }

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
                Messenger.SendMessage(MessageAddress + nameof(Multiplier), Multiplier);
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
            Messenger.SendEnabled = false;
            ChanceToHit.Paste(beatmodifierdto.ChanceToHit);
            Multiplier = beatmodifierdto.Multiplier;
            Messenger.SendEnabled = true;
        }
        #endregion
    }
}