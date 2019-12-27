using System;
using CMiX.MVVM.Models;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class GeometryFXModel : IModel
    {
        public GeometryFXModel()
        {
            Explode = new SliderModel();
            FileSelector = new FileSelectorModel();
        }

        public SliderModel Explode { get; set; }
        public FileSelectorModel FileSelector { get; set; }
        public string MessageAddress { get; set; }
    }
}