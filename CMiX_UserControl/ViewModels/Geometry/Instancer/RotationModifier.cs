using System;
using System.Windows;
using System.Collections.ObjectModel;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using Memento;

namespace CMiX.ViewModels
{
    [Serializable]
    public class RotationModifier : ViewModel
    {
        public RotationModifier(string messageaddress, ObservableCollection<ServerValidation> serverValidations, Mementor mementor, Beat beat)
            : base(serverValidations, mementor)
        {
            MessageAddress = String.Format("{0}{1}/", messageaddress, nameof(RotationModifier));

            Rotation = new AnimParameter(MessageAddress + nameof(Rotation), serverValidations, mementor, beat, true);
            RotationX = new AnimParameter(MessageAddress + nameof(RotationX), serverValidations, mementor, beat, false);
            RotationY = new AnimParameter(MessageAddress + nameof(RotationY), serverValidations, mementor, beat, false);
            RotationZ = new AnimParameter(MessageAddress + nameof(RotationZ), serverValidations, mementor, beat, false);
        }

        #region PROPERTIES
        public AnimParameter Rotation { get; set; }
        public AnimParameter RotationX { get; set; }
        public AnimParameter RotationY { get; set; }
        public AnimParameter RotationZ { get; set; }
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
                DisabledMessages();

                var Rotationmodifiermodel = data.GetData("RotationModifierModel") as RotationModifierModel;
                var messageaddress = MessageAddress;
                this.Paste(Rotationmodifiermodel);
                UpdateMessageAddress(messageaddress);
                this.Copy(Rotationmodifiermodel);

                EnabledMessages();
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
            DisabledMessages();

            MessageAddress = Rotationmodifiermodel.MessageAddress;

            Rotation.Paste(Rotationmodifiermodel.Rotation);
            RotationX.Paste(Rotationmodifiermodel.RotationX);
            RotationY.Paste(Rotationmodifiermodel.RotationY);
            RotationZ.Paste(Rotationmodifiermodel.RotationZ);

            EnabledMessages();
        }

        public void Reset()
        {
            DisabledMessages();

            Rotation.Reset();
            RotationX.Reset();
            RotationY.Reset();
            RotationZ.Reset();

            EnabledMessages();

            RotationModifierModel Rotationmodifiermodel = new RotationModifierModel();
            this.Copy(Rotationmodifiermodel);
            //SendMessages(MessageAddress, Rotationmodifiermodel);
            //QueueObjects(Rotationmodifiermodel);
            //SendQueues();
        }
        #endregion
    }
}
