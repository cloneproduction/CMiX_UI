using System;
using System.Windows;
using System.Collections.ObjectModel;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using Memento;

namespace CMiX.ViewModels
{
    [Serializable]
    public class TranslateModifier : ViewModel
    {
        public TranslateModifier(string messageaddress, ObservableCollection<OSCValidation> oscvalidation, Mementor mementor, Beat beat) 
            : base(oscvalidation, mementor)
        {
            MessageAddress = String.Format("{0}{1}/", messageaddress, nameof(TranslateModifier));

            Translate = new AnimParameter(MessageAddress + nameof(Translate), oscvalidation, mementor, beat, true);
            TranslateX = new AnimParameter(MessageAddress + nameof(TranslateX), oscvalidation, mementor, beat, false);
            TranslateY = new AnimParameter(MessageAddress + nameof(TranslateY), oscvalidation, mementor, beat, false);
            TranslateZ = new AnimParameter(MessageAddress + nameof(TranslateZ), oscvalidation, mementor, beat, false);
        }

        #region PROPERTIES
        public AnimParameter Translate { get; set; }
        public AnimParameter TranslateX { get; set; }
        public AnimParameter TranslateY { get; set; }
        public AnimParameter TranslateZ { get; set; }
        #endregion

        #region METHODS
        public void UpdateMessageAddress(string messageaddress)
        {
            MessageAddress = messageaddress;

            Translate.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(Translate)));
            TranslateX.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(TranslateX)));
            TranslateY.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(TranslateY)));
            TranslateZ.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(TranslateZ)));
        }
        #endregion

        #region COPY/PASTE/RESET
        public void CopyGeometry()
        {
            TranslateModifierModel translatemodifiermodel = new TranslateModifierModel();
            this.Copy(translatemodifiermodel);
            IDataObject data = new DataObject();
            data.SetData("TranslateModifierModel", translatemodifiermodel, false);
            Clipboard.SetDataObject(data);
        }

        public void PasteGeometry()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("TranslateModifierModel"))
            {
                Mementor.BeginBatch();
                DisabledMessages();

                var translatemodifiermodel = data.GetData("TranslateModifierModel") as TranslateModifierModel;
                var messageaddress = MessageAddress;
                this.Paste(translatemodifiermodel);
                UpdateMessageAddress(messageaddress);
                this.Copy(translatemodifiermodel);

                EnabledMessages();
                Mementor.EndBatch();

                QueueObjects(translatemodifiermodel);
                SendQueues();
            }
        }

        public void ResetGeometry()
        {
            TranslateModifierModel translatemodifiermodel = new TranslateModifierModel();
            this.Reset();
            this.Copy(translatemodifiermodel);
            QueueObjects(translatemodifiermodel);
            SendQueues();
        }

        public void Copy(TranslateModifierModel translatemodifiermodel)
        {
            translatemodifiermodel.MessageAddress = MessageAddress;
            Translate.Copy(translatemodifiermodel.Translate);
            TranslateX.Copy(translatemodifiermodel.TranslateX);
            TranslateY.Copy(translatemodifiermodel.TranslateY);
            TranslateZ.Copy(translatemodifiermodel.TranslateZ);
        }

        public void Paste(TranslateModifierModel translatemodifiermodel)
        {
            DisabledMessages();

            MessageAddress = translatemodifiermodel.MessageAddress;

            Translate.Paste(translatemodifiermodel.Translate);
            TranslateX.Paste(translatemodifiermodel.TranslateX);
            TranslateY.Paste(translatemodifiermodel.TranslateY);
            TranslateZ.Paste(translatemodifiermodel.TranslateZ);

            EnabledMessages();
        }

        public void Reset()
        {
            DisabledMessages();

            Translate.Reset();
            TranslateX.Reset();
            TranslateY.Reset();
            TranslateZ.Reset();

            EnabledMessages();

            TranslateModifierModel translatemodifiermodel = new TranslateModifierModel();
            this.Copy(translatemodifiermodel);
            QueueObjects(translatemodifiermodel);
            SendQueues();
        }
        #endregion
    }
}
