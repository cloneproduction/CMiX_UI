using CMiX.MVVM.Controls;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Mediator;

namespace CMiX.MVVM.ViewModels
{
    public class Camera : Sender
    {
        public Camera(string name, IColleague parentSender, MasterBeat beat) :base (name, parentSender)
        {
            Rotation = ((CameraRotation)0).ToString();
            LookAt = ((CameraLookAt)0).ToString();
            View = ((CameraView)0).ToString();

            BeatModifier = new BeatModifier(nameof(BeatModifier), this, beat);
            FOV = new Slider(nameof(FOV), this);
            Zoom = new Slider(nameof(Zoom), this);
        }

        public override void Receive(Message message)
        {
            this.SetViewModel(message.Obj as CameraModel);
        }
        public BeatModifier BeatModifier { get; set; }
        public Slider FOV { get; set; }
        public Slider Zoom { get; set; }

        private string _rotation;
        public string Rotation
        {
            get => _rotation;
            set => SetAndNotify(ref _rotation, value);
        }

        private string _lookAt;
        public string LookAt
        {
            get => _lookAt;
            set => SetAndNotify(ref _lookAt, value);
        }

        private string _view;
        public string View
        {
            get => _view;
            set => SetAndNotify(ref _view, value);
        }
    }
}