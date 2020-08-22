using CMiX.MVVM.Models;

namespace CMiX.MVVM.ViewModels
{
    public static class AnimParameterModelFactory
    {
        public static AnimParameterModel GetModel(this AnimParameter instance)
        {
            AnimParameterModel model = new AnimParameterModel();
            model.IsEnabled = instance.IsEnabled;
            model.Name = instance.Name;
            model.AnimMode = instance.AnimMode;
            model.EasingModel = instance.Easing.GetModel();
            model.RangeModel = instance.Range.GetModel();
            model.BeatModifierModel = instance.BeatModifier.GetModel();
            return model;
        }

        public static void SetViewModel(this AnimParameter instance, AnimParameterModel model)
        {
            instance.AnimMode = model.AnimMode;
            instance.Name = model.Name;
            instance.IsEnabled = model.IsEnabled;
            instance.Easing.SetViewModel(model.EasingModel);
            instance.Range.SetViewModel(model.RangeModel);
            instance.BeatModifier.SetViewModel(model.BeatModifierModel);
        }
    }
}