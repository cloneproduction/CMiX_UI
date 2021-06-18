using CMiX.MVVM.Interfaces;
using System;

namespace CMiX.MVVM.Models
{
    public class GeometryFXModel : Model, IModel
    {
        public GeometryFXModel()
        {
            this.ID = Guid.NewGuid();
            Explode = new SliderModel();
        }

        public SliderModel Explode { get; set; }
    }
}