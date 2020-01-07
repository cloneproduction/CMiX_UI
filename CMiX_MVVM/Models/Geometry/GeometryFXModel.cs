using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class GeometryFXModel
    {
        public GeometryFXModel()
        {
            Explode = new SliderModel();
            FileSelector = new FileSelectorModel();
        }

        public SliderModel Explode { get; set; }
        public FileSelectorModel FileSelector { get; set; }
    }
}