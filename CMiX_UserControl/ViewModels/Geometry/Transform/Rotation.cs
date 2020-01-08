using System.Windows;
using Memento;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.Studio.ViewModels
{
    public class Rotation : ViewModel, ISendable, IUndoable
    {
        public Rotation(string messageaddress, MessageService messageService, Mementor mementor)
        {
            MessageAddress = $"{messageaddress}{nameof(Rotation)}/";
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
                MessageService.Disable();

                var rotationmodel = data.GetData("RotationModel") as RotationModel;
                var messageaddress = MessageAddress;
                this.Paste(rotationmodel);
                //UpdateMessageAddress(messageaddress);
                this.Copy(rotationmodel);

                MessageService.Enable();
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
            RotationX.CopyModel(rotationmodel.RotationX);
            RotationY.CopyModel(rotationmodel.RotationY);
            RotationZ.CopyModel(rotationmodel.RotationZ);
        }

        public void Paste(RotationModel rotationmodel)
        {
            MessageService.Disable();

            RotationX.PasteModel(rotationmodel.RotationX);
            RotationY.PasteModel(rotationmodel.RotationY);
            RotationZ.PasteModel(rotationmodel.RotationZ);

            MessageService.Enable();
        }

        public void Reset()
        {
            MessageService.Disable();
            //Mementor.BeginBatch();

            RotationX.Reset();
            RotationY.Reset();
            RotationZ.Reset();

            //Mementor.EndBatch();
            MessageService.Enable();

            RotationModel rotationmodel = new RotationModel();
            this.Copy(rotationmodel);
            //SendMessages(nameof(RotationModel), rotationmodel);
        }
        #endregion
    }
}