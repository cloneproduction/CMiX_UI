namespace CMiX.ViewModels
{
    public class Camera : ViewModel
    {
        CameraRotation _Rotation;
        public CameraRotation Rotation
        {
            get => _Rotation;
            set => this.SetAndNotify(ref _Rotation, value);
        }

        CameraLookAt _LookAt;
        public CameraLookAt LookAt
        {
            get => _LookAt;
            set => this.SetAndNotify(ref _LookAt, value);
        }

        CameraView _View;
        public CameraView View
        {
            get => _View;
            set => this.SetAndNotify(ref _View, value);
        }


        int _BeatMultiplier = 1;
        public int BeatMultiplier
        {
            get => _BeatMultiplier;
            set => this.SetAndNotify(ref _BeatMultiplier, value);
        }

        double _BeatChanceToHit = 1.0;
        public double BeatChanceToHit
        {
            get => _BeatChanceToHit;
            set => this.SetAndNotify(ref _BeatChanceToHit, value);
        }

        double _CameraFOV = 1.0;
        public double CameraFOV
        {
            get => _CameraFOV;
            set => this.SetAndNotify(ref _CameraFOV, value);
        }

        double _CameraZoom = 1.0;
        public double CameraZoom
        {
            get => _CameraZoom;
            set => this.SetAndNotify(ref _CameraZoom, value);
        }
    }
}
