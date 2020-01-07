using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class TranslateModel
    {
        public TranslateModel()
        {
            TranslateX = new SliderModel();
            TranslateY = new SliderModel();
            TranslateZ = new SliderModel();
        }

        public SliderModel TranslateX { get; set; }
        public SliderModel TranslateY { get; set; }
        public SliderModel TranslateZ { get; set; }
    }
}