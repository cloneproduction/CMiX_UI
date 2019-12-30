using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using Memento;
using System;
using System.Windows;

namespace CMiX.ViewModels
{
    public class RotationModifier : ViewModel, ISendable, IUndoable
    {
        public RotationModifier(string messageaddress, MessageService messageService, Mementor mementor, Beat beat)
        {
            MessageAddress = String.Format("{0}{1}/", messageaddress, nameof(RotationModifier));
            MessageService = messageService;
            Rotation = new AnimParameter(MessageAddress + nameof(Rotation), messageService, mementor, beat, true);
            RotationX = new AnimParameter(MessageAddress + nameof(RotationX), messageService, mementor, beat, false);
            RotationY = new AnimParameter(MessageAddress + nameof(RotationY), messageService, mementor, beat, false);
            RotationZ = new AnimParameter(MessageAddress + nameof(RotationZ), messageService, mementor, beat, false);
        }

        #region PROPERTIES
        public AnimParameter Rotation { get; set; }
        public AnimParameter RotationX { get; set; }
        public AnimParameter RotationY { get; set; }
        public AnimParameter RotationZ { get; set; }
        public string MessageAddress { get; set; }
        public MessageService MessageService { get; set; }
        public Mementor Mementor { get; set; }
        #endregion

        #region METHODS
        public void UpdateMessageAddress(string messageaddress)
        {
            MessageAddress = messageaddress;

            Rotation.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(Rotation)));
            RotationX.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(RotationX)));
            RotationY.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(RotationY)));
            RotationZ.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(RotationZ)));
        }
        #endregion

        #region COPY/PASTE/RESET
        public void CopyGeometry()
        {
            RotationModifierModel Rotationmodifiermodel = new RotationModifierModel();
            this.Copy(Rotationmodifiermodel);
            IDataObject data = new DataObject();
            data.SetData("RotationModifierModel", Rotationmodifiermodel, false);
            Clipboard.SetDataObject(data);
        }

        public void PasteGeometry()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("RotationModifierModel"))
            {
                Mementor.BeginBatch();
                MessageService.DisabledMessages();

                var Rotationmodifiermodel = data.GetData("RotationModifierModel") as RotationModifierModel;
                var messageaddress = MessageAddress;
                this.Paste(Rotationmodifiermodel);
                UpdateMessageAddress(messageaddress);
                this.Copy(Rotationmodifiermodel);

                MessageService.EnabledMessages();
                Mementor.EndBatch();
                //SendMessages(MessageAddress, Rotationmodifiermodel);
                //QueueObjects(Rotationmodifiermodel);
                //SendQueues();
            }
        }

        public void ResetGeometry()
        {
            RotationModifierModel Rotationmodifiermodel = new RotationModifierModel();
            this.Reset();
            this.Copy(Rotationmodifiermodel);
            //SendMessages(MessageAddress, Rotationmodifiermodel);
            //QueueObjects(Rotationmodifiermodel);
            //SendQueues();
        }

        public void Copy(RotationModifierModel Rotationmodifiermodel)
        {
            Rotationmodifiermodel.MessageAddress = MessageAddress;
            Rotation.Copy(Rotationmodifiermodel.Rotation);
            RotationX.Copy(Rotationmodifiermodel.RotationX);
            RotationY.Copy(Rotationmodifiermodel.RotationY);
            RotationZ.Copy(Rotationmodifiermodel.RotationZ);
        }

        public void Paste(RotationModifierModel Rotationmodifiermodel)
        {
            MessageService.DisabledMessages();

            MessageAddress = Rotationmodifiermodel.MessageAddress;

            Rotation.Paste(Rotationmodifiermodel.Rotation);
            RotationX.Paste(Rotationmodifiermodel.RotationX);
            RotationY.Paste(Rotationmodifiermodel.RotationY);
            RotationZ.Paste(Rotationmodifiermodel.RotationZ);

            MessageService.EnabledMessages();
        }

        public void Reset()
        {
            MessageService.DisabledMessages();

            Rotation.Reset();
            RotationX.Reset();
            RotationY.Reset();
            RotationZ.Reset();

            MessageService.EnabledMessages();

            RotationModifierModel Rotationmodifiermodel = new RotationModifierModel();
            this.Copy(Rotationmodifiermodel);
            //SendMessages(MessageAddress, Rotationmodifiermodel);
            //QueueObjects(Rotationmodifiermodel);
            //SendQueues();
        }
        #endregion
    }
}
