using System.Windows;
using Memento;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.Studio.ViewModels
{
    public class Rotation : ViewModel, IUndoable
    {
        public Rotation(string messageaddress, Mementor mementor)
        {
            MessageAddress = $"{messageaddress}{nameof(Rotation)}/";

            X = new Slider(MessageAddress + nameof(X), mementor);
            Y = new Slider(MessageAddress + nameof(Y), mementor);
            Z = new Slider(MessageAddress + nameof(Z), mementor);
        }

        public Slider X { get; set; }
        public Slider Y { get; set; }
        public Slider Z { get; set; }

        public string MessageAddress { get ; set ; }
        public MessengerService MessengerService { get; set; }
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
                MessengerService.Disable();

                var rotationmodel = data.GetData("RotationModel") as RotationModel;
                var messageaddress = MessageAddress;
                this.Paste(rotationmodel);

                MessengerService.Enable();
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
            MessengerService.Disable();

            X.SetViewModel(rotationmodel.X);
            Y.SetViewModel(rotationmodel.Y);
            Z.SetViewModel(rotationmodel.Z);

            MessengerService.Enable();
        }

        public void Reset()
        {
            MessengerService.Disable();
            //Mementor.BeginBatch();

            X.Reset();
            Y.Reset();
            Z.Reset();

            //Mementor.EndBatch();
            MessengerService.Enable();

            //SendMessages(nameof(RotationModel), GetModel());
        }
        #endregion
    }
}