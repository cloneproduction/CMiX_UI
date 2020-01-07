using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class PostFXModel
    {
        public PostFXModel()
        {
            Feedback = new SliderModel();
            Blur = new SliderModel();
        }


        public SliderModel Feedback { get; set; }
        public SliderModel Blur { get; set; }

        public string Transforms { get; set; }
        public string View { get; set; }
    }
}