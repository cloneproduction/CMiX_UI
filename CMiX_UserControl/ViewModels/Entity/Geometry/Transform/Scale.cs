using System.Windows;
using Memento;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;

namespace CMiX.Studio.ViewModels
{
    public class Scale : ViewModel
    {
        public Scale(string messageaddress) 
        {
            MessageAddress = $"{messageaddress}{nameof(Scale)}/";

            X = new Slider(MessageAddress + nameof(X));
            Y = new Slider(MessageAddress + nameof(Y));
            Z = new Slider(MessageAddress + nameof(Z));
        }

        public Slider X { get; set; }
        public Slider Y { get; set; }
        public Slider Z { get; set; }
        public string MessageAddress { get ; set; }
        public MessengerService MessengerService { get; set; }


        #region COPY/PASTE/RESET
        public void CopyGeometry()
        {
            IDataObject data = new DataObject();
            data.SetData("ScaleModel", this.GetModel(), false);
            Clipboard.SetDataObject(data);
        }

        public void PasteGeometry()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("ScaleModel"))
            {
                //Mementor.BeginBatch();
                MessengerService.Disable();

                var scalemodel = data.GetData("ScaleModel") as ScaleModel;
                var messageaddress = MessageAddress;
                this.Paste(scalemodel);

                MessengerService.Enable();
                //Mementor.EndBatch();
                //SendMessages(nameof(ScaleModel), GetModel());
            }
        }

        public void ResetGeometry()
        {
            ScaleModel scalemodel = this.GetModel();
            this.Reset();
            //SendMessages(nameof(ScaleModel), scalemodel);
        }

        public void Paste(ScaleModel scalemodel)
        {
            MessengerService.Disable();

            X.SetViewModel(scalemodel.X);
            Y.SetViewModel(scalemodel.Y);
            Z.SetViewModel(scalemodel.Z);
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

            ScaleModel scalemodel = this.GetModel();
            //SendMessages(nameof(ScaleModel), scalemodel);
        }
        #endregion
    }
}
