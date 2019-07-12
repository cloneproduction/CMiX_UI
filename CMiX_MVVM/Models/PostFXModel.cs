//using CMiX.Services;
using CMiX.MVVM.Models;
using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class PostFXModel : Model
    {
        public PostFXModel()
        {
            Feedback = new SliderModel();
            Blur = new SliderModel();
        }

        public SliderModel Feedback { get; set; }
        public SliderModel Blur { get; set; }

        //[OSC]
        public string Transforms { get; set; }

        //[OSC]
        public string View { get; set; }
    }
}
