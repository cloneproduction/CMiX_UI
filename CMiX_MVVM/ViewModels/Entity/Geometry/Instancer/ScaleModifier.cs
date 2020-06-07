using CMiX.MVVM.Models;
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

        public AnimParameter Scale { get; set; }
        public AnimParameter ScaleX { get; set; }
        public AnimParameter ScaleY { get; set; }
        public AnimParameter ScaleZ { get; set; }


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
    }
}