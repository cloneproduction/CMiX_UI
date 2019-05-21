using System;

namespace CMiX.Models
{
    [Serializable]
    public class GeometryFXModel
    {
        public GeometryFXModel()
        {
            Explode = new SliderModel();
        }
        public string MessageAddress { get; set; }
        public SliderModel Explode { get; set; }
    }
}