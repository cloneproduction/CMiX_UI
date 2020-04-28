using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class GeometryFXModel
    {
        public GeometryFXModel()
        {
            Explode = new SliderModel();
        }

        public SliderModel Explode { get; set; }
    }
}