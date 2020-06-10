using CMiX.MVVM.Models;

namespace CMiX.MVVM.ViewModels
{
    public static class CameraModelFactory
    {
        public static CameraModel GetModel(this Camera instance)
        {
            CameraModel cameraModel = new CameraModel();

            cameraModel.Rotation = instance.Rotation;
            cameraModel.LookAt = instance.LookAt;
            cameraModel.View = instance.View;

            return cameraModel;
        }

        public static void SetViewModel(this Camera instance, CameraModel cameraModel)
        {
            instance.Rotation = cameraModel.Rotation;
            instance.LookAt = cameraModel.LookAt;
            instance.View = cameraModel.View;

            instance.BeatModifier.SetViewModel(cameraModel.BeatModifierModel);
            instance.FOV.SetViewModel(cameraModel.FOV);
            instance.Zoom.SetViewModel(cameraModel.Zoom);
        }
    }
}