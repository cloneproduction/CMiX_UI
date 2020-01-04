using System.Windows;
using Memento;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.Studio.ViewModels
{
    public class Rotation : ViewModel, ISendable, IUndoable
    {
        public Rotation(string messageaddress, Messenger messenger, Mementor mementor)
        {
            MessageAddress = $"{messageaddress}{nameof(Rotation)}/";
            Messenger = messenger;
            RotationX = new Slider(MessageAddress + nameof(RotationX), messenger, mementor);
            RotationY = new Slider(MessageAddress + nameof(RotationY), messenger, mementor);
            RotationZ = new Slider(MessageAddress + nameof(RotationZ), messenger, mementor);
        }

        public Slider RotationX { get; set; }
        public Slider RotationY { get; set; }
        public Slider RotationZ { get; set; }
        public string MessageAddress { get ; set ; }
        public Messenger Messenger { get; set; }
        public Mementor Mementor { get; set; }

        public void UpdateMessageAddress(string messageaddress)
        {
            MessageAddress = messageaddress;
            RotationX.UpdateMessageAddress($"{messageaddress}{nameof(RotationX)}/");
            RotationY.UpdateMessageAddress($"{messageaddress}{nameof(RotationY)}/");
            RotationZ.UpdateMessageAddress($"{messageaddress}{nameof(RotationZ)}/");
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
                Messenger.Disable();

                var rotationmodel = data.GetData("RotationModel") as RotationModel;
                var messageaddress = MessageAddress;
                this.Paste(rotationmodel);
                UpdateMessageAddress(messageaddress);
                this.Copy(rotationmodel);

                Messenger.Enable();
                Mementor.EndBatch();
                //SendMessages(nameof(RotationModel), rotationmodel);
            }
        }

        public void ResetGeometry()
        {
            RotationModel rotationmodel = new RotationModel();
            this.Reset();
            this.Copy(rotationmodel);
            //SendMessages(nameof(RotationModel), rotationmodel);
        }

        public void Copy(RotationModel rotationmodel)
        {
            rotationmodel.MessageAddress = MessageAddress;
            RotationX.CopyModel(rotationmodel.RotationX);
            RotationY.CopyModel(rotationmodel.RotationY);
            RotationZ.CopyModel(rotationmodel.RotationZ);
        }

        public void Paste(RotationModel rotationmodel)
        {
            Messenger.Disable();

            MessageAddress = rotationmodel.MessageAddress;
            RotationX.PasteModel(rotationmodel.RotationX);
            RotationY.PasteModel(rotationmodel.RotationY);
            RotationZ.PasteModel(rotationmodel.RotationZ);
            Messenger.Enable();
        }

        public void Reset()
        {
            Messenger.Disable();
            //Mementor.BeginBatch();

            RotationX.Reset();
            RotationY.Reset();
            RotationZ.Reset();

            //Mementor.EndBatch();
            Messenger.Enable();

            RotationModel rotationmodel = new RotationModel();
            this.Copy(rotationmodel);
            //SendMessages(nameof(RotationModel), rotationmodel);
        }
        #endregion
    }
}