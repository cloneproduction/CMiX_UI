using CMiX.MVVM.Models;
using System.Windows;

namespace CMiX.MVVM.ViewModels
{
    public class RotationModifier : ViewModel
    {
        public RotationModifier(Beat beat)
        {
            Rotation = new AnimParameter(nameof(Rotation), beat, true);
            RotationX = new AnimParameter(nameof(RotationX), beat, false);
            RotationY = new AnimParameter(nameof(RotationY), beat, false);
            RotationZ = new AnimParameter(nameof(RotationZ), beat, false);
        }


        public AnimParameter Rotation { get; set; }
        public AnimParameter RotationX { get; set; }
        public AnimParameter RotationY { get; set; }
        public AnimParameter RotationZ { get; set; }


        public void CopyGeometry()
        {
            IDataObject data = new DataObject();
            data.SetData("RotationModifierModel", this.GetModel(), false);
            Clipboard.SetDataObject(data);
        }

        public void PasteGeometry()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("RotationModifierModel"))
            {
                var Rotationmodifiermodel = data.GetData("RotationModifierModel") as RotationModifierModel;
            }
        }
    }
}
