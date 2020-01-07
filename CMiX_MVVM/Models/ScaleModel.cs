using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class ScaleModel
    {
        public ScaleModel()
        {
            ScaleX = new SliderModel();
            ScaleY = new SliderModel();
            ScaleZ = new SliderModel();
        }

        public SliderModel ScaleX { get; set; }
        public SliderModel ScaleY { get; set; }
        public SliderModel ScaleZ { get; set; }
    }
}
