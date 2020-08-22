using CMiX.MVVM.Models;

namespace CMiX.MVVM.ViewModels
{
    public static class EasingModelFactory
    {
        public static EasingModel GetModel(this Easing instance)
        {
            EasingModel model = new EasingModel();
            model.EasingFunction = instance.EasingFunction;
            model.EasingMode = instance.EasingMode;
            return model;
        }

        public static void SetViewModel(this Easing instance, EasingModel model)
        {
            instance.EasingFunction = model.EasingFunction;
            instance.EasingMode = model.EasingMode;
        }
    }
}