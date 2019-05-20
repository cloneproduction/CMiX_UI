using System;

namespace CMiX.Models
{
    [Serializable]
    public class PostFXModel
    {
        public PostFXModel()
        {
            Feedback = new SliderModel();
            Blur = new SliderModel();
        }
        public string MessageAddress { get; set; }
        public SliderModel Feedback { get; set; }
        public SliderModel Blur { get; set; }
        public string Transforms { get; set; }
        public string View { get; set; }
    }
}
