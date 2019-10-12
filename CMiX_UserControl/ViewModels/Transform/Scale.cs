using System;
using System.Collections.ObjectModel;
using System.Windows;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using Memento;

namespace CMiX.ViewModels
{
    [Serializable]
    public class Scale : ViewModel
    {
        public Scale(string messageaddress, ObservableCollection<OSCValidation> oscvalidation, Mementor mementor) 
            : base (oscvalidation, mementor)
        {
            MessageAddress = String.Format("{0}{1}/", messageaddress, nameof(Scale));
            ScaleX = new Slider(MessageAddress + nameof(ScaleX), oscvalidation, mementor);
            ScaleY = new Slider(MessageAddress + nameof(ScaleY), oscvalidation, mementor);
            ScaleZ = new Slider(MessageAddress + nameof(ScaleZ), oscvalidation, mementor);
        }

        public Slider ScaleX { get; set; }
        public Slider ScaleY { get; set; }
        public Slider ScaleZ { get; set; }

        public void UpdateMessageAddress(string messageaddress)
        {
            MessageAddress = messageaddress;
            ScaleX.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(ScaleX)));
            ScaleY.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(ScaleY)));
            ScaleZ.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(ScaleZ)));
        }

        #region COPY/PASTE/RESET
        public void CopyGeometry()
        {
            ScaleModel scalemodel = new ScaleModel();
            this.Copy(scalemodel);
            IDataObject data = new DataObject();
            data.SetData("ScaleModel", scalemodel, false);
            Clipboard.SetDataObject(data);
        }

        public void PasteGeometry()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("ScaleModel"))
            {
                Mementor.BeginBatch();
                DisabledMessages();

                var scalemodel = data.GetData("ScaleModel") as ScaleModel;
                var messageaddress = MessageAddress;
                this.Paste(scalemodel);
                UpdateMessageAddress(messageaddress);
                this.Copy(scalemodel);

                EnabledMessages();
                Mementor.EndBatch();

                QueueObjects(scalemodel);
                SendQueues();
            }
        }

        public void ResetGeometry()
        {
            ScaleModel scalemodel = new ScaleModel();
            this.Reset();
            this.Copy(scalemodel);
            QueueObjects(scalemodel);
            SendQueues();
        }

        public void Copy(ScaleModel scalemodel)
        {
            scalemodel.MessageAddress = MessageAddress;
            ScaleX.Copy(scalemodel.ScaleX);
            ScaleY.Copy(scalemodel.ScaleY);
            ScaleZ.Copy(scalemodel.ScaleZ);
        }

        public void Paste(ScaleModel scalemodel)
        {
            DisabledMessages();

            MessageAddress = scalemodel.MessageAddress;
            ScaleX.Paste(scalemodel.ScaleX);
            ScaleY.Paste(scalemodel.ScaleY);
            ScaleZ.Paste(scalemodel.ScaleZ);
            EnabledMessages();
        }

        public void Reset()
        {
            DisabledMessages();
            //Mementor.BeginBatch();

            ScaleX.Reset();
            ScaleY.Reset();
            ScaleZ.Reset();

            //Mementor.EndBatch();
            EnabledMessages();

            ScaleModel scalemodel = new ScaleModel();
            this.Copy(scalemodel);
            QueueObjects(scalemodel);
            SendQueues();
        }
        #endregion
    }
}
