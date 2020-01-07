using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class ScaleModifierModel
    {
        public ScaleModifierModel()
        {
            Scale = new AnimParameterModel();
            ScaleX = new AnimParameterModel();
            ScaleY = new AnimParameterModel();
            ScaleZ = new AnimParameterModel();
        }

        public AnimParameterModel Scale { get; set; }
        public AnimParameterModel ScaleX { get; set; }
        public AnimParameterModel ScaleY { get; set; }
        public AnimParameterModel ScaleZ { get; set; }
    }
}