using CMiX.MVVM.Models;

namespace CMiX.MVVM.ViewModels
{
    public static class BlendModelModelFactory
    {
        public static BlendModeModel GetModel(this BlendMode instance)
        {
            BlendModeModel blendModeModel = new BlendModeModel();
            blendModeModel.Mode = instance.Mode;
            return blendModeModel;
        }

        public static void SetViewModel(this BlendMode instance, BlendModeModel blendModeModel)
        {
            instance.Mode = blendModeModel.Mode;
        }
    }
}
