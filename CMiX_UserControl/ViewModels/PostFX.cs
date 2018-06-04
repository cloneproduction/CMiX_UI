using System;

namespace CMiX.ViewModels
{
    public class PostFX : ViewModel
    {
        public PostFX()
            : this(feedback: 0.0, blur: 0.0, transforms: default, view: default)
        { }

        public PostFX(double feedback, double blur, PostFXTransforms transforms, PostFXView view)
        {
            if (feedback < 0)
            {
                throw new ArgumentException("Feedback must not be negative.");
            }

            if (blur < 0)
            {
                throw new ArgumentException("Blur must not be negative.");
            }

            Feedback = feedback;
            Blur = blur;
            Transforms = transforms;
            View = view;
        }

        private double _feedback;
        public double Feedback
        {
            get => _feedback;
            set
            {
                if (value < 0)
                {
                    value = 0;
                }

                SetAndNotify(ref _feedback, value);
            }
        }

        private double _blur;
        public double Blur
        {
            get => _blur;
            set
            {
                if (value < 0)
                {
                    value = 0;
                }

                SetAndNotify(ref _blur, value);
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
