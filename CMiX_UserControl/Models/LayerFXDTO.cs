using System;

namespace CMiX.Models
{
    [Serializable]
    public class LayerFXDTO
    {
        public string MessageAddress { get; set; }
        public double Feedback { get; set; }
        public double Blur { get; set; }
    }
}
