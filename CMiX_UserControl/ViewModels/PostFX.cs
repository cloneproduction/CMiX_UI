namespace CMiX.ViewModels
{
    public class PostFX : ViewModel
    {
        double _FeedBack = 0.0;
        public double FeedBack
        {
            get => _FeedBack;
            set => SetAndNotify(ref _FeedBack, value);
        }

        double _Blur = 0.0;
        public double Blur
        {
            get => _Blur;
            set => SetAndNotify(ref _Blur, value);
        }

        PostFXTransforms _Transforms;
        public PostFXTransforms Transforms
        {
            get => _Transforms;
            set => SetAndNotify(ref _Transforms, value);
        }

        PostFXView _View;
        public PostFXView View
        {
            get => _View;
            set => SetAndNotify(ref _View, value);
        }
    }
}
