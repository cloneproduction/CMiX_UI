using CMiX.MVVM.Models;

namespace CMiX.MVVM.ViewModels
{
    public static class AnimModeModelFactory
    {
        public static AnimModeModel GetModel(this AnimMode instance)
        {
            AnimModeModel model = new AnimModeModel();
            model.Mode = instance.Mode ;
            return model;
        }

        public static void SetViewModel(this AnimMode instance, AnimModeModel model)
        {
            instance.Mode = model.Mode;
        }
    }
}
