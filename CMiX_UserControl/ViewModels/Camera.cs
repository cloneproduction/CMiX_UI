namespace CMiX.ViewModels
{
    public class Camera : ViewModel
    {
        public Camera()
            : this(
                  rotation: default,
                  lookAt: default,
                  view: default,
                  beatMultiplier: 1,
                  beatChanceToHit: 1.0,
                  cameraFOV: 1.0,
                  cameraZoom: 1.0)
        { }

        public Camera(CameraRotation rotation, CameraLookAt lookAt, CameraView view, int beatMultiplier, double beatChanceToHit, double cameraFOV, double cameraZoom)
        {
            Rotation = rotation;
            LookAt = lookAt;
            View = view;
            BeatMultiplier = beatMultiplier;
            BeatChanceToHit = beatChanceToHit;
            CameraFOV = cameraFOV;
            CameraZoom = cameraZoom;
        }

        CameraRotation _Rotation;
        public CameraRotation Rotation
        {
            get => _Rotation;
            set => SetAndNotify(ref _Rotation, value);
        }

        CameraLookAt _LookAt;
        public CameraLookAt LookAt
        {
            get => _LookAt;
            set => SetAndNotify(ref _LookAt, value);
        }

        CameraView _View;
        public CameraView View
        {
            get => _View;
            set => SetAndNotify(ref _View, value);
        }

        int _BeatMultiplier = 1;
        public int BeatMultiplier
        {
            get => _BeatMultiplier;
            set => SetAndNotify(ref _BeatMultiplier, value);
        }

        double _BeatChanceToHit = 1.0;
        public double BeatChanceToHit
        {
            get => _BeatChanceToHit;
            set => SetAndNotify(ref _BeatChanceToHit, value);
        }

        double _CameraFOV = 1.0;
        public double CameraFOV
        {
            get => _CameraFOV;
            set => SetAndNotify(ref _CameraFOV, value);
        }

        double _CameraZoom = 1.0;
        public double CameraZoom
        {
            get => _CameraZoom;
            set => SetAndNotify(ref _CameraZoom, value);
        }
    }
}
