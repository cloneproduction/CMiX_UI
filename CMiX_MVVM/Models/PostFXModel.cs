using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels;
using System;

namespace CMiX.MVVM.Models
{
    public class PostFXModel : Model, IModel
    {
        public PostFXModel()
        {
            ID = Guid.NewGuid();
            Feedback = new SliderModel();
            Blur = new SliderModel();

            Transforms = ((PostFXTransforms)0).ToString();
            View = ((PostFXView)0).ToString();
        }

        public SliderModel Feedback { get; set; }
        public SliderModel Blur { get; set; }

        public string Transforms { get; set; }
        public string View { get; set; }
    }
}