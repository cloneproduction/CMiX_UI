namespace CMiX.MVVM.ViewModels
{
    public class Camera : ViewModel
    {
        public Camera(Beat beat) 
        {
            Rotation = ((CameraRotation)0).ToString();
            LookAt = ((CameraLookAt)0).ToString();
            View = ((CameraView)0).ToString();

            BeatModifier = new BeatModifier(beat);
            FOV = new Slider(nameof(FOV));
            Zoom = new Slider(nameof(Zoom));
        }

        public BeatModifier BeatModifier { get; }
        public Slider FOV { get; }
        public Slider Zoom { get; }

        private string _rotation;
        public string Rotation
        {
            get => _rotation;
            set
            {
                SetAndNotify(ref _rotation, value);
                //SendMessages(MessageAddress + nameof(Rotation), Rotation);
            }
        }

        private string _lookAt;
        public string LookAt
        {
            get => _lookAt;
            set
            {
                SetAndNotify(ref _lookAt, value);
                //SendMessages(MessageAddress + nameof(LookAt), LookAt);
            }
        }

        private string _view;
        public string View
        {
            get => _view;
            set
            {
                SetAndNotify(ref _view, value);
                //SendMessages(MessageAddress + nameof(View), View);
            }
        }
    }
}