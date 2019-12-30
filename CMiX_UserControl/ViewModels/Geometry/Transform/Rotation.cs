using System;
using System.Windows;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using Memento;
using CMiX.MVVM.Services;

namespace CMiX.ViewModels
{
    public class Rotation : ViewModel, ISendable, IUndoable
    {
        public Rotation(string messageaddress, MessageService messageService, Mementor mementor)
        {
            MessageAddress = String.Format("{0}{1}/", messageaddress, nameof(Rotation));
            MessageService = messageService;
            RotationX = new Slider(MessageAddress + nameof(RotationX), messageService, mementor);
            RotationY = new Slider(MessageAddress + nameof(RotationY), messageService, mementor);
            RotationZ = new Slider(MessageAddress + nameof(RotationZ), messageService, mementor);
        }

        public Slider RotationX { get; set; }
        public Slider RotationY { get; set; }
        public Slider RotationZ { get; set; }
        public string MessageAddress { get ; set ; }
        public MessageService MessageService { get; set; }
        public Mementor Mementor { get; set; }

        public void UpdateMessageAddress(string messageaddress)
        {
            MessageAddress = messageaddress;
            RotationX.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(RotationX)));
            RotationY.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(RotationY)));
            RotationZ.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(RotationZ)));
        }

        #region COPY/PASTE/RESET
        public void CopyGeometry()
        {
            RotationModel rotationmodel = new RotationModel();
            this.Copy(rotationmodel);
            IDataObject data = new DataObject();
            data.SetData("RotationModel", rotationmodel, false);
            Clipboard.SetDataObject(data);
        }

        public void PasteGeometry()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("RotationModel"))
            {
                Mementor.BeginBatch();
                MessageService.DisabledMessages();

                var rotationmodel = data.GetData("RotationModel") as RotationModel;
                var messageaddress = MessageAddress;
                this.Paste(rotationmodel);
                UpdateMessageAddress(messageaddress);
                this.Copy(rotationmodel);

                MessageService.EnabledMessages();
                Mementor.EndBatch();
                //SendMessages(nameof(RotationModel), rotationmodel);
                //QueueObjects(rotationmodel);
                //SendQueues();
            }
        }

        public void ResetGeometry()
        {
            RotationModel rotationmodel = new RotationModel();
            this.Reset();
            this.Copy(rotationmodel);
            //SendMessages(nameof(RotationModel), rotationmodel);
            //QueueObjects(rotationmodel);
            //SendQueues();
        }

        public void Copy(RotationModel rotationmodel)
        {
            rotationmodel.MessageAddress = MessageAddress;
            RotationX.Copy(rotationmodel.RotationX);
            RotationY.Copy(rotationmodel.RotationY);
            RotationZ.Copy(rotationmodel.RotationZ);
        }

        public void Paste(RotationModel rotationmodel)
        {
            MessageService.DisabledMessages();

            MessageAddress = rotationmodel.MessageAddress;
            RotationX.Paste(rotationmodel.RotationX);
            RotationY.Paste(rotationmodel.RotationY);
            RotationZ.Paste(rotationmodel.RotationZ);
            MessageService.EnabledMessages();
        }

        public void Reset()
        {
            MessageService.DisabledMessages();
            //Mementor.BeginBatch();

            RotationX.Reset();
            RotationY.Reset();
            RotationZ.Reset();

            //Mementor.EndBatch();
            MessageService.EnabledMessages();

            RotationModel rotationmodel = new RotationModel();
            this.Copy(rotationmodel);
            //SendMessages(nameof(RotationModel), rotationmodel);
            //QueueObjects(rotationmodel);
            //SendQueues();
        }
        #endregion
    }
}