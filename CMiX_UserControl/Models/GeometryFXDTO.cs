using System;

namespace CMiX.Models
{
    [Serializable]
    public class GeometryFXDTO
    {
        public GeometryFXDTO()
        {
            Explode = new SliderDTO();
        }
        public SliderDTO Explode { get; set; }
    }
}