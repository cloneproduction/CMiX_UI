using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using System;
using System.Collections.ObjectModel;
using Memento;
using CMiX.MVVM.Services;
using CMiX.MVVM;

namespace CMiX.Studio.ViewModels
{
    public class BeatModifier : Beat, ISendable, IUndoable
    {
        #region CONSTRUCTORS
        public BeatModifier(string messageAddress, Beat beat, Sender sender, Mementor mementor)
        {
            MessageAddress = $"{messageAddress}{nameof(BeatModifier)}/";
            Sender = sender;

            MasterBeat = beat;
            Multiplier = 1.0;
            ChanceToHit = new Slider(MessageAddress + nameof(ChanceToHit), sender, mementor)
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

        //#region METHODS
        //public void UpdateMessageAddress(string messageaddress)
        //{
        //    MessageAddress = messageaddress;
        //    ChanceToHit.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(ChanceToHit)));
        //}
        //#endregion

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
        public Sender Sender { get; set; }
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
            Sender.Disable();

            Multiplier = 1.0;
            ChanceToHit.Reset();
            ChanceToHit.Amount = 1.0;

            Sender.Enable();
        }

        public void CopyModel(BeatModifierModel beatModifierModel)
        {
            ChanceToHit.CopyModel(beatModifierModel.ChanceToHit);
            beatModifierModel.Multiplier = Multiplier;
        }

        public void PasteModel(BeatModifierModel beatModifierModel)
        {
            Sender.Disable();

            Multiplier = beatModifierModel.Multiplier;
            ChanceToHit.PasteModel(beatModifierModel.ChanceToHit);

            Sender.Enable();
        }
        #endregion
    }
}