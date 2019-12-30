using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using Memento;
using System;
using System.Windows;

namespace CMiX.ViewModels
{
    public class Scale : ViewModel, ISendable, IUndoable
    {
        public Scale(string messageaddress, MessageService messageService, Mementor mementor) 
        {
            MessageAddress = String.Format("{0}{1}/", messageaddress, nameof(Scale));
            MessageService = messageService;

            ScaleX = new Slider(MessageAddress + nameof(ScaleX), messageService, mementor);
            ScaleY = new Slider(MessageAddress + nameof(ScaleY), messageService, mementor);
            ScaleZ = new Slider(MessageAddress + nameof(ScaleZ), messageService, mementor);
        }

        public Slider ScaleX { get; set; }
        public Slider ScaleY { get; set; }
        public Slider ScaleZ { get; set; }
        public string MessageAddress { get ; set; }
        public MessageService MessageService { get; set; }
        public Mementor Mementor { get; set; }

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
                MessageService.DisabledMessages();

                var scalemodel = data.GetData("ScaleModel") as ScaleModel;
                var messageaddress = MessageAddress;
                this.Paste(scalemodel);
                UpdateMessageAddress(messageaddress);
                this.Copy(scalemodel);

                MessageService.EnabledMessages();
                Mementor.EndBatch();
                //SendMessages(nameof(ScaleModel), scalemodel);
            }
        }

        public void ResetGeometry()
        {
            ScaleModel scalemodel = new ScaleModel();
            this.Reset();
            this.Copy(scalemodel);
            //SendMessages(nameof(ScaleModel), scalemodel);
            //QueueObjects(scalemodel);
            //SendQueues();
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
            MessageService.DisabledMessages();

            MessageAddress = scalemodel.MessageAddress;
            ScaleX.Paste(scalemodel.ScaleX);
            ScaleY.Paste(scalemodel.ScaleY);
            ScaleZ.Paste(scalemodel.ScaleZ);
            MessageService.EnabledMessages();
        }

        public void Reset()
        {
            MessageService.DisabledMessages();
            //Mementor.BeginBatch();

            ScaleX.Reset();
            ScaleY.Reset();
            ScaleZ.Reset();

            //Mementor.EndBatch();
            MessageService.EnabledMessages();

            ScaleModel scalemodel = new ScaleModel();
            this.Copy(scalemodel);
            //SendMessages(nameof(ScaleModel), scalemodel);
            //QueueObjects(scalemodel);
            //SendQueues();
        }
        #endregion
    }
}
