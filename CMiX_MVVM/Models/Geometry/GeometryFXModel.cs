using CMiX.MVVM.Interfaces;
using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class GeometryFXModel : Model, IModel
    {
        public GeometryFXModel()
        {
            Explode = new SliderModel();
        }

        public SliderModel Explode { get; set; }
    }
}