using System.Windows;
using Memento;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public class Rotation : ViewModel
    {
        public Rotation()
        {
            //MessageAddress = $"{messageaddress}{nameof(Rotation)}/";

            X = new Slider(nameof(X));
            Y = new Slider(nameof(Y));
            Z = new Slider(nameof(Z));
        }

        public Slider X { get; set; }
        public Slider Y { get; set; }
        public Slider Z { get; set; }

        public string MessageAddress { get ; set ; }

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
                //Mementor.BeginBatch();
                var rotationmodel = data.GetData("RotationModel") as RotationModel;
                var messageaddress = MessageAddress;
                this.Paste(rotationmodel);
                //Mementor.EndBatch();
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
            X.SetViewModel(rotationmodel.X);
            Y.SetViewModel(rotationmodel.Y);
            Z.SetViewModel(rotationmodel.Z);
        }

        public void Reset()
        {
            //Mementor.BeginBatch();

            X.Reset();
            Y.Reset();
            Z.Reset();

            //Mementor.EndBatch();

            //SendMessages(nameof(RotationModel), GetModel());
        }
        #endregion
    }
}