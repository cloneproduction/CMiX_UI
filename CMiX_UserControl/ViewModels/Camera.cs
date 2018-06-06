using CMiX.Services;
using System;

namespace CMiX.ViewModels
{
    public class Camera : ViewModel
    {
        public Camera(IMessenger messenger)
            : this(
                  messenger: messenger,
                  rotation: default(CameraRotation),
                  lookAt: default(CameraLookAt),
                  view: default(CameraView),
                  beatModifier: new BeatModifier(),
                  fov: 0.5,
                  zoom: 1.0)
        {
        }

        public Camera(IMessenger messenger, CameraRotation rotation, CameraLookAt lookAt, CameraView view, BeatModifier beatModifier, double fov, double zoom)
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

        private Messenger _message;
        public Messenger Message
        {
            get => _message;
            set => SetAndNotify(ref _message, value);
        }

        private CameraRotation _rotation;
        public CameraRotation Rotation
        {
            get => _rotation;
            set => SetAndNotify(ref _rotation, value);
        }

        private CameraLookAt _lookAt;
        public CameraLookAt LookAt
        {
            get => _lookAt;
            set => SetAndNotify(ref _lookAt, value);
        }

        private CameraView _view;
        public CameraView View
        {
            get => _view;
            set => SetAndNotify(ref _view, value);
        }

        public BeatModifier BeatModifier { get; }

        private double _FOV;
        public double FOV
        {
            get => _FOV;
            set
            {
                SetAndNotify(ref _FOV, value);
                Messenger.SendMessage("/CameraFOV", FOV);
            }
        }

        private double _zoom;
        public double Zoom
        {
            get => _zoom;
            set
            {
                SetAndNotify(ref _zoom, value);
                Messenger.SendMessage("/CameraZoom", Zoom);
            }
        }
    }
}
