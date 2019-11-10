using System;
using System.Collections.ObjectModel;
using System.Windows;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using Memento;

namespace CMiX.ViewModels
{
    public class Rotation : ViewModel
    {
        public Rotation(string messageaddress, ObservableCollection<ServerValidation> serverValidations, Mementor mementor)
            : base(serverValidations, mementor)
        {
            MessageAddress = String.Format("{0}{1}/", messageaddress, nameof(Rotation));
            RotationX = new Slider(MessageAddress + nameof(RotationX), serverValidations, mementor);
            RotationY = new Slider(MessageAddress + nameof(RotationY), serverValidations, mementor);
            RotationZ = new Slider(MessageAddress + nameof(RotationZ), serverValidations, mementor);
        }

        public Slider RotationX { get; set; }
        public Slider RotationY { get; set; }
        public Slider RotationZ { get; set; }

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
                DisabledMessages();

                var rotationmodel = data.GetData("RotationModel") as RotationModel;
                var messageaddress = MessageAddress;
                this.Paste(rotationmodel);
                UpdateMessageAddress(messageaddress);
                this.Copy(rotationmodel);

                EnabledMessages();
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
            DisabledMessages();

            MessageAddress = rotationmodel.MessageAddress;
            RotationX.Paste(rotationmodel.RotationX);
            RotationY.Paste(rotationmodel.RotationY);
            RotationZ.Paste(rotationmodel.RotationZ);
            EnabledMessages();
        }

        public void Reset()
        {
            DisabledMessages();
            //Mementor.BeginBatch();

            RotationX.Reset();
            RotationY.Reset();
            RotationZ.Reset();

            //Mementor.EndBatch();
            EnabledMessages();

            RotationModel rotationmodel = new RotationModel();
            this.Copy(rotationmodel);
            //SendMessages(nameof(RotationModel), rotationmodel);
            //QueueObjects(rotationmodel);
            //SendQueues();
        }
        #endregion
    }
}