using CMiX.MVVM.Models;

namespace CMiX.MVVM.ViewModels
{
    public static class AnimModeModelFactory
    {
        public static AnimModeModel GetModel(this AnimMode instance)
        {
            AnimModeModel model = new AnimModeModel();
            model.ModeType = instance.ModeType ;
            return model;
        }

        public static void SetViewModel(this AnimMode instance, AnimModeModel model)
        {
            instance.ModeType = model.ModeType;
        }
    }
}
