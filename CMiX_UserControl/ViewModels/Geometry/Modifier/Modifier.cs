using System;
using System.Windows;
using System.Collections.ObjectModel;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using Memento;

namespace CMiX.ViewModels
{
    [Serializable]
    public class Modifier : ViewModel
    {
        public Modifier(string messageaddress, ObservableCollection<OSCValidation> oscvalidation, Mementor mementor, Beat beat)
            : base(oscvalidation, mementor)
        {
            MessageAddress = String.Format("{0}{1}/", messageaddress, nameof(Modifier));

            Counter = new Counter(MessageAddress, oscvalidation, mementor);
            TranslateModifier = new TranslateModifier(MessageAddress, oscvalidation, mementor, beat);
            ScaleModifier = new ScaleModifier(MessageAddress, oscvalidation, mementor, beat);
            RotationModifier = new RotationModifier(MessageAddress, oscvalidation, mementor, beat);

            NoAspectRatio = false;
        }

        public Counter Counter { get; }
        public TranslateModifier TranslateModifier { get; }
        public ScaleModifier ScaleModifier { get; }
        public RotationModifier RotationModifier { get; }

        private bool _noAspectRatio;
        public bool NoAspectRatio
        {
            get => _noAspectRatio;
            set
            {
                if (Mementor != null)
                    Mementor.PropertyChange(this, "NoAspectRatio");
                SetAndNotify(ref _noAspectRatio, value);
                SendMessages(MessageAddress + nameof(NoAspectRatio), NoAspectRatio.ToString());
            }
        }

        #region METHODS
        public void UpdateMessageAddress(string messageaddress)
        {
            MessageAddress = messageaddress;

            Counter.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(Counter)));
            TranslateModifier.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(TranslateModifier)));
            ScaleModifier.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(ScaleModifier)));
            RotationModifier.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(RotationModifier)));
        }
        #endregion

        #region COPY/PASTE/RESET
        public void CopyGeometry()
        {
            ModifierModel modifiermodel = new ModifierModel();
            this.Copy(modifiermodel);
            IDataObject data = new DataObject();
            data.SetData("ModifierModel", modifiermodel, false);
            Clipboard.SetDataObject(data);
        }

        public void PasteGeometry()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("ModifierModel"))
            {
                Mementor.BeginBatch();
                DisabledMessages();

                var modifiermodel = data.GetData("ModifierModel") as ModifierModel;
                var geometrymessageaddress = MessageAddress;
                this.Paste(modifiermodel);
                UpdateMessageAddress(geometrymessageaddress);
                this.Copy(modifiermodel);

                EnabledMessages();
                Mementor.EndBatch();

                QueueObjects(modifiermodel);
                SendQueues();
            }
        }

        public void ResetGeometry()
        {
            ModifierModel modifiermodel = new ModifierModel();
            this.Reset();
            this.Copy(modifiermodel);
            QueueObjects(modifiermodel);
            SendQueues();
        }

        public void Copy(ModifierModel modifiermodel)
        {
            modifiermodel.MessageAddress = MessageAddress;
            Counter.Copy(modifiermodel.Counter);
            TranslateModifier.Copy(modifiermodel.TranslateModifier);
            ScaleModifier.Copy(modifiermodel.ScaleModifier);
            RotationModifier.Copy(modifiermodel.RotationModifier);
            modifiermodel.NoAspectRatio = NoAspectRatio;
        }

        public void Paste(ModifierModel modifiermodel)
        {
            DisabledMessages();

            MessageAddress = modifiermodel.MessageAddress;
            Counter.Paste(modifiermodel.Counter);
            TranslateModifier.Paste(modifiermodel.TranslateModifier);
            ScaleModifier.Paste(modifiermodel.ScaleModifier);
            RotationModifier.Paste(modifiermodel.RotationModifier);
            NoAspectRatio = modifiermodel.NoAspectRatio;

            EnabledMessages();
        }

        public void Reset()
        {
            DisabledMessages();
            //Mementor.BeginBatch();
            Counter.Reset();
            TranslateModifier.Reset();
            ScaleModifier.Reset();
            RotationModifier.Reset();
            NoAspectRatio = false;
            //Mementor.EndBatch();
            EnabledMessages();

            ModifierModel modifiermodel = new ModifierModel();
            this.Copy(modifiermodel);
            QueueObjects(modifiermodel);
            SendQueues();
        }
        #endregion
    }
}
