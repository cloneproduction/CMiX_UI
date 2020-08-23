using CMiX.MVVM.Models;

namespace CMiX.MVVM.ViewModels
{
    public static class BlendModelModelFactory
    {
        public static BlendModeModel GetModel(this BlendMode instance)
        {
            BlendModeModel model = new BlendModeModel();
            model.Mode = instance.Mode;
            return model;
        }

        public static void SetViewModel(this BlendMode instance, BlendModeModel model)
        {
            instance.Mode = model.Mode;
        }
    }
}
