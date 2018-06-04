using System;

namespace CMiX.ViewModels
{
    public class Camera : ViewModel
    {
        public Camera()
            : this(
                  rotation: default,
                  lookAt: default,
                  view: default,
                  beatModifier: new BeatModifier(),
                  fov: 1.0,
                  zoom: 1.0)
        { }

        public Camera(CameraRotation rotation, CameraLookAt lookAt, CameraView view, BeatModifier beatModifier, double fov, double zoom)
        {
            Rotation = rotation;
            LookAt = lookAt;
            View = view;
            BeatModifier = beatModifier ?? throw new ArgumentNullException(nameof(beatModifier));
            FOV = fov;
            Zoom = zoom;
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
            set => SetAndNotify(ref _FOV, value);
        }

        private double _zoom;
        public double Zoom
        {
            get => _zoom;
            set => SetAndNotify(ref _zoom, value);
        }
    }
}
