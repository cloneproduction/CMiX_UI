using CMiX.Core.Models;
using System;

namespace CMiX.Core.Models
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