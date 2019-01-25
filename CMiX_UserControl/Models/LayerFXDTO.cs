using System;

namespace CMiX.Models
{
    [Serializable]
    public class LayerFXDTO
    {
        public LayerFXDTO()
        {
            Feedback = new SliderDTO();
            Blur = new SliderDTO();
        }

        public string MessageAddress { get; set; }
        public SliderDTO Feedback { get; set; }
        public SliderDTO Blur { get; set; }
    }
}
