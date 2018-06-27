using System;
using CMiX.Services;

namespace CMiX.ViewModels
{
    public class Camera : ViewModel
    {
        public Camera(IMessenger messenger, MasterBeat masterBeat)
            : this(
                  messenger: messenger,
                  rotation: ((CameraRotation)0).ToString(),
                  lookAt: ((CameraLookAt)0).ToString(),
                  view: ((CameraView)0).ToString(),
                  beatModifier: new BeatModifier(masterBeat, "/Camera", messenger),
                  fov: 0.5,
                  zoom: 1.0)
        {
        }

        public Camera(IMessenger messenger, string rotation, string lookAt, string view, BeatModifier beatModifier, double fov, double zoom)
        {
            Messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
            Rotation = rotation;
            LookAt = lookAt;
            View = view;
            BeatModifier = beatModifier ?? throw new ArgumentNullException(nameof(beatModifier));
            FOV = fov;
            Zoom = zoom;
        }

        public IMessenger Messenger { get; }

        private string _rotation;
        public string Rotation
        {
            get => _rotation;
            set
            {
                SetAndNotify(ref _rotation, value);
                Messenger.SendMessage("/Camera/Rotation", Rotation);
            }
        }

        private string _lookAt;
        public string LookAt
        {
            get => _lookAt;
            set
            {
                SetAndNotify(ref _lookAt, value);
                Messenger.SendMessage("/Camera/LookAt", LookAt);
            }
        }

        private string _view;
        public string View
        {
            get => _view;
            set
            {
                SetAndNotify(ref _view, value);
                Messenger.SendMessage("/Camera/View", View);
            }
        }

        public BeatModifier BeatModifier { get; }

        private double _FOV;
        public double FOV
        {
            get => _FOV;
            set
            {
                SetAndNotify(ref _FOV, value);
                Messenger.SendMessage("/Camera/FOV", FOV);
            }
        }

        private double _zoom;
        public double Zoom
        {
            get => _zoom;
            set
            {
                SetAndNotify(ref _zoom, value);
                Messenger.SendMessage("/Camera/Zoom", Zoom);
            }
        }
    }
}