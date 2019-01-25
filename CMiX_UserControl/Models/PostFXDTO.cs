using System;

namespace CMiX.Models
{
    [Serializable]
    public class PostFXDTO
    {
        public PostFXDTO()
        {
            Feedback = new SliderDTO();
            Blur = new SliderDTO();
        }
        public string MessageAddress { get; set; }
        public SliderDTO Feedback { get; set; }
        public SliderDTO Blur { get; set; }
        public string Transforms { get; set; }
        public string View { get; set; }
    }
}
