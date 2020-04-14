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
            X = new Slider(MessageAddress + nameof(X), messageService, mementor);
            Y = new Slider(MessageAddress + nameof(Y), messageService, mementor);
            Z = new Slider(MessageAddress + nameof(Z), messageService, mementor);
        }

        public Slider X { get; set; }
        public Slider Y { get; set; }
        public Slider Z { get; set; }

        public string MessageAddress { get ; set ; }
        public MessageService MessageService { get; set; }
        public Mementor Mementor { get; set; }

        #region COPY/PASTE/RESET
        public void CopyGeometry()
        {
            IDataObject data = new DataObject();
            data.SetData("RotationModel", this.GetModel(), false);
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

                MessageService.Enable();
                Mementor.EndBatch();
                //SendMessages(nameof(RotationModel), GetModel());
            }
        }

        public void ResetGeometry()
        {
            this.Reset();
            //SendMessages(nameof(RotationModel), GetModel());
        }




        public void Paste(RotationModel rotationmodel)
        {
            MessageService.Disable();

            X.SetViewModel(rotationmodel.X);
            Y.SetViewModel(rotationmodel.Y);
            Z.SetViewModel(rotationmodel.Z);

            MessageService.Enable();
        }

        public void Reset()
        {
            MessageService.Disable();
            //Mementor.BeginBatch();

            X.Reset();
            Y.Reset();
            Z.Reset();

            //Mementor.EndBatch();
            MessageService.Enable();

            //SendMessages(nameof(RotationModel), GetModel());
        }
        #endregion
    }
}