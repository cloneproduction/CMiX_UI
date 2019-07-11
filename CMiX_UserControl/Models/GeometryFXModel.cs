using System;
using CMiX.MVVM.Models;

namespace CMiX.Models
{
    [Serializable]
    public class GeometryFXModel : Model
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