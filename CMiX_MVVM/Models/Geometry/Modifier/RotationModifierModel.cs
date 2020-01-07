using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class RotationModifierModel
    {
        public RotationModifierModel()
        {
            Rotation = new AnimParameterModel();
            RotationX = new AnimParameterModel();
            RotationY = new AnimParameterModel();
            RotationZ = new AnimParameterModel();
        }

        public AnimParameterModel Rotation { get; set; }
        public AnimParameterModel RotationX { get; set; }
        public AnimParameterModel RotationY { get; set; }
        public AnimParameterModel RotationZ { get; set; }
    }
}