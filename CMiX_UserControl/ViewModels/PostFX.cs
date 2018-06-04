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
            AssertNotNegative(() => feedback);
            AssertNotNegative(() => blur);

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
                SetAndNotify(ref _feedback, CoerceNotNegative(value));
            }
        }

        private double _blur;
        public double Blur
        {
            get => _blur;
            set
            {
                SetAndNotify(ref _blur, CoerceNotNegative(value));
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
