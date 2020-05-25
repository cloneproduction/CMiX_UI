using System.Windows;
using CMiX.MVVM.Models;

namespace CMiX.MVVM.ViewModels
{
    public class Transform : Sendable
    {
        public Transform()
        {
            Translate = new Translate();
            Scale = new Scale();
            Rotation = new Rotation();

            Is3D = false;
        }

        #region PROPERTY
        public Translate Translate { get; }
        public Scale Scale { get; }
        public Rotation Rotation { get; }

        private bool _is3D;
        public bool Is3D
        {
            get => _is3D;
            set
            {
                //if (Mementor != null)
                //    Mementor.PropertyChange(this, "Is3D");
                SetAndNotify(ref _is3D, value);
            }
        }
        #endregion

        #region COPY/PASTE/RESET
        public void CopyGeometry()
        {
            IDataObject data = new DataObject();
            data.SetData("TransformModel", this.GetModel(), false);
            Clipboard.SetDataObject(data);
        }

        public void PasteGeometry()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("TransformModel"))
            {
                var transformmodel = data.GetData("TransformModel") as TransformModel;
                this.Paste(transformmodel);
            }
        }

        public void ResetGeometry()
        {
            this.Reset();
        }

        public void Paste(TransformModel transformModel)
        {
            Translate.Paste(transformModel.TranslateModel);
            Scale.Paste(transformModel.ScaleModel);
            Rotation.Paste(transformModel.RotationModel);
            Is3D = transformModel.Is3D;  
        }

        public void Reset()
        {
            Translate.Reset();
            Scale.Reset();
            Rotation.Reset();
            Is3D = false;
        }
        #endregion
    }
}