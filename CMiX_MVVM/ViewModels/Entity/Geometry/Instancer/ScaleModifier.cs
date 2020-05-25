using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using Memento;
using System;
using System.Windows;

namespace CMiX.MVVM.ViewModels
{
    [Serializable]
    public class ScaleModifier : ViewModel
    {
        public ScaleModifier(Beat beat)
        {
            Scale = new AnimParameter(nameof(Scale), beat, true);
            ScaleX = new AnimParameter(nameof(ScaleX), beat, false);
            ScaleY = new AnimParameter(nameof(ScaleY), beat, false);
            ScaleZ = new AnimParameter(nameof(ScaleZ), beat, false);
        }

        #region PROPERTIES
        public AnimParameter Scale { get; set; }
        public AnimParameter ScaleX { get; set; }
        public AnimParameter ScaleY { get; set; }
        public AnimParameter ScaleZ { get; set; }
        #endregion

        #region COPY/PASTE/RESET
        public void CopyGeometry()
        {
            IDataObject data = new DataObject();
            data.SetData("ScaleModifierModel", this.GetModel(), false);
            Clipboard.SetDataObject(data);
        }

        public void PasteGeometry()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("ScaleModifierModel"))
            {
                var Scalemodifiermodel = data.GetData("ScaleModifierModel") as ScaleModifierModel;
            }
        }

        public void ResetGeometry()
        {
            this.Reset();
        }

        public void Reset()
        {
            Scale.Reset();
            ScaleX.Reset();
            ScaleY.Reset();
            ScaleZ.Reset();
        }
        #endregion
    }
}