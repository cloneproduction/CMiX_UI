using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using Memento;
using System;
using System.Windows;

namespace CMiX.Studio.ViewModels
{
    public class RotationModifier : ViewModel, ISendable, IUndoable
    {
        public RotationModifier(string messageaddress, Messenger messenger, Mementor mementor, Beat beat)
        {
            MessageAddress = String.Format("{0}{1}/", messageaddress, nameof(RotationModifier));
            Messenger = messenger;
            Rotation = new AnimParameter(MessageAddress + nameof(Rotation), messenger, mementor, beat, true);
            RotationX = new AnimParameter(MessageAddress + nameof(RotationX), messenger, mementor, beat, false);
            RotationY = new AnimParameter(MessageAddress + nameof(RotationY), messenger, mementor, beat, false);
            RotationZ = new AnimParameter(MessageAddress + nameof(RotationZ), messenger, mementor, beat, false);
        }

        #region PROPERTIES
        public AnimParameter Rotation { get; set; }
        public AnimParameter RotationX { get; set; }
        public AnimParameter RotationY { get; set; }
        public AnimParameter RotationZ { get; set; }
        public string MessageAddress { get; set; }
        public Messenger Messenger { get; set; }
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
                Messenger.Disable();

                var Rotationmodifiermodel = data.GetData("RotationModifierModel") as RotationModifierModel;
                var messageaddress = MessageAddress;
                this.Paste(Rotationmodifiermodel);
                UpdateMessageAddress(messageaddress);
                this.Copy(Rotationmodifiermodel);

                Messenger.Enable();
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
            Messenger.Disable();

            MessageAddress = Rotationmodifiermodel.MessageAddress;

            Rotation.Paste(Rotationmodifiermodel.Rotation);
            RotationX.Paste(Rotationmodifiermodel.RotationX);
            RotationY.Paste(Rotationmodifiermodel.RotationY);
            RotationZ.Paste(Rotationmodifiermodel.RotationZ);

            Messenger.Enable();
        }

        public void Reset()
        {
            Messenger.Disable();

            Rotation.Reset();
            RotationX.Reset();
            RotationY.Reset();
            RotationZ.Reset();

            Messenger.Enable();

            RotationModifierModel Rotationmodifiermodel = new RotationModifierModel();
            this.Copy(Rotationmodifiermodel);
            //SendMessages(MessageAddress, Rotationmodifiermodel);
            //QueueObjects(Rotationmodifiermodel);
            //SendQueues();
        }
        #endregion
    }
}
