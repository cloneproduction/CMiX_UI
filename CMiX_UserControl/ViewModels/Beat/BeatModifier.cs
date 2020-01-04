using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using System;
using System.Collections.ObjectModel;
using Memento;
using CMiX.MVVM.Services;
using CMiX.MVVM;

namespace CMiX.ViewModels
{
    public class BeatModifier : Beat, ICopyPasteModel, ISendable, IUndoable
    {
        #region CONSTRUCTORS
        public BeatModifier(string messageaddress, Beat beat, Messenger messenger, Mementor mementor)
        {
            MessageAddress = String.Format("{0}{1}/", messageaddress, nameof(BeatModifier));
            Messenger = messenger;

            MasterBeat = beat;
            Multiplier = 1.0;
            ChanceToHit = new Slider(MessageAddress + nameof(ChanceToHit), messenger, mementor)
            {
                Amount = 1.0
            };
            beat.PeriodChanged += (s, newvalue) =>
            {
                OnPeriodChanged(Period);
                Notify(nameof(Period));
                Notify(nameof(BPM));
            };
        }
        #endregion

        #region METHODS
        public void UpdateMessageAddress(string messageaddress)
        {
            MessageAddress = messageaddress;
            ChanceToHit.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(ChanceToHit)));
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

        public override double Multiplier
        {
            get => base.Multiplier;
            set
            {
                
                if (Mementor != null)
                    Mementor.PropertyChange(this, "Multiplier");                   
                base.Multiplier = value;
                OnPeriodChanged(Period);
                Notify(nameof(Period));
                Notify(nameof(BPM));
                //SendMessages(MessageAddress + nameof(Multiplier), Multiplier);
            }
        }

        public string MessageAddress { get; set; }
        public Messenger Messenger { get; set; }
        public Mementor Mementor { get; set; }
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

        #region COPY/PASTE/RESET
        public void Reset()
        {
            Messenger.Disable();

            Multiplier = 1.0;
            ChanceToHit.Reset();
            ChanceToHit.Amount = 1.0;

            Messenger.Enable();
        }

        public void CopyModel(IModel model)
        {
            BeatModifierModel beatModifierModel = model as BeatModifierModel;
            beatModifierModel.MessageAddress = MessageAddress;
            ChanceToHit.CopyModel(beatModifierModel.ChanceToHit);
            beatModifierModel.Multiplier = Multiplier;
        }

        public void PasteModel(IModel model)
        {
            Messenger.Disable();

            BeatModifierModel beatModifierModel = model as BeatModifierModel;
            MessageAddress = beatModifierModel.MessageAddress;
            Multiplier = beatModifierModel.Multiplier;
            ChanceToHit.PasteModel(beatModifierModel.ChanceToHit);

            Messenger.Enable();
        }
        #endregion
    }
}