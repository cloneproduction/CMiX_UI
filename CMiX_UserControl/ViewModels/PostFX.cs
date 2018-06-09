using System;

namespace CMiX.ViewModels
{
    public class PostFX : ViewModel
    {
        public PostFX(string layerName)
            : this(
                  layerName : layerName,
                  message: new Messenger(),
                  feedback: 0.0, 
                  blur: 0.0, 
                  transforms: default, 
                  view: default)
        { }

        public PostFX(
            string layerName,
            Messenger message,
            double feedback, 
            double blur, 
            PostFXTransforms transforms, 
            PostFXView view)
        {
            AssertNotNegative(() => feedback);
            AssertNotNegative(() => blur);

            LayerName = layerName;
            Message = message;
            Feedback = feedback;
            Blur = blur;
            Transforms = transforms;
            View = view;
        }

        private string Address => String.Format("{0}/{1}/", LayerName, nameof(PostFX));

        private Messenger _message;
        public Messenger Message
        {
            get => _message;
            set => SetAndNotify(ref _message, value);
        }

        private string _layerName;
        public string LayerName
        {
            get => _layerName;
            set => SetAndNotify(ref _layerName, value);
        }

        private double _feedback;
        public double Feedback
        {
            get => _feedback;
            set
            {
                SetAndNotify(ref _feedback, CoerceNotNegative(value));
                Message.SendOSC(Address + nameof(Feedback), Feedback.ToString());
            }
        }

        private double _blur;
        public double Blur
        {
            get => _blur;
            set
            {
                SetAndNotify(ref _blur, CoerceNotNegative(value));
                Message.SendOSC(Address + nameof(Blur), Blur.ToString());
            }
        }

        private PostFXTransforms _transforms;
        public PostFXTransforms Transforms
        {
            get => _transforms;
            set => SetAndNotify(ref _transforms, value);
        }

        private PostFXView _view;
        public PostFXView View
        {
            get => _view;
            set => SetAndNotify(ref _view, value);
        }
    }
}
