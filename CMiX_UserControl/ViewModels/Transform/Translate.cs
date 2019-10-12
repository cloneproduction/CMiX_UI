using System;
using System.Collections.ObjectModel;
using System.Windows;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using Memento;

namespace CMiX.ViewModels
{
    public class Translate : ViewModel
    {
        public Translate(string messageaddress, ObservableCollection<OSCValidation> oscvalidation, Mementor mementor)
            : base(oscvalidation, mementor)
        {
            MessageAddress = String.Format("{0}{1}/", messageaddress, nameof(Translate));
            TranslateX = new Slider(MessageAddress + nameof(TranslateX), oscvalidation, mementor);
            TranslateY = new Slider(MessageAddress + nameof(TranslateY), oscvalidation, mementor);
            TranslateZ = new Slider(MessageAddress + nameof(TranslateZ), oscvalidation, mementor);
        }

        public Slider TranslateX { get; set; }
        public Slider TranslateY { get; set; }
        public Slider TranslateZ { get; set; }

        public void UpdateMessageAddress(string messageaddress)
        {
            MessageAddress = messageaddress;
            TranslateX.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(TranslateX)));
            TranslateY.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(TranslateY)));
            TranslateZ.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(TranslateZ)));
        }

        #region COPY/PASTE/RESET
        public void CopyGeometry()
        {
            TranslateModel translatemodel = new TranslateModel();
            this.Copy(translatemodel);
            IDataObject data = new DataObject();
            data.SetData("TranslateModel", translatemodel, false);
            Clipboard.SetDataObject(data);
        }

        public void PasteGeometry()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("TranslateModel"))
            {
                Mementor.BeginBatch();
                DisabledMessages();

                var translatemodel = data.GetData("TranslateModel") as TranslateModel;
                var messageaddress = MessageAddress;
                this.Paste(translatemodel);
                UpdateMessageAddress(messageaddress);
                this.Copy(translatemodel);

                EnabledMessages();
                Mementor.EndBatch();

                QueueObjects(translatemodel);
                SendQueues();
            }
        }

        public void ResetGeometry()
        {
            TranslateModel translatemodel = new TranslateModel();
            this.Reset();
            this.Copy(translatemodel);
            QueueObjects(translatemodel);
            SendQueues();
        }

        public void Copy(TranslateModel translatemodel)
        {
            translatemodel.MessageAddress = MessageAddress;
            TranslateX.Copy(translatemodel.TranslateX);
            TranslateY.Copy(translatemodel.TranslateY);
            TranslateZ.Copy(translatemodel.TranslateZ);
        }

        public void Paste(TranslateModel translatemodel)
        {
            DisabledMessages();

            MessageAddress = translatemodel.MessageAddress;
            TranslateX.Paste(translatemodel.TranslateX);
            TranslateY.Paste(translatemodel.TranslateY);
            TranslateZ.Paste(translatemodel.TranslateZ);
            EnabledMessages();
        }

        public void Reset()
        {
            DisabledMessages();
            //Mementor.BeginBatch();

            TranslateX.Reset();
            TranslateY.Reset();
            TranslateZ.Reset();

            //Mementor.EndBatch();
            EnabledMessages();

            TranslateModel translatemodel = new TranslateModel();
            this.Copy(translatemodel);
            QueueObjects(translatemodel);
            SendQueues();
        }
        #endregion
    }
}
