using System.Windows;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;

namespace CMiX.Studio.ViewModels
{
    public class Scale : ViewModel
    {
        public Scale() 
        {
            X = new Slider(nameof(X));
            Y = new Slider(nameof(Y));
            Z = new Slider(nameof(Z));
        }

        public Slider X { get; set; }
        public Slider Y { get; set; }
        public Slider Z { get; set; }

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
                var scalemodel = data.GetData("ScaleModel") as ScaleModel;
                this.Paste(scalemodel);
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
            X.SetViewModel(scalemodel.X);
            Y.SetViewModel(scalemodel.Y);
            Z.SetViewModel(scalemodel.Z);
        }

        public void Reset()
        {
            //Mementor.BeginBatch();

            X.Reset();
            Y.Reset();
            Z.Reset();

            //Mementor.EndBatch();

            ScaleModel scalemodel = this.GetModel();
            //SendMessages(nameof(ScaleModel), scalemodel);
        }
        #endregion
    }
}
