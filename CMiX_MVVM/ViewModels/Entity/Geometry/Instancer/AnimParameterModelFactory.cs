using CMiX.MVVM.Models;

namespace CMiX.MVVM.ViewModels
{
    public static class AnimParameterModelFactory
    {
        public static AnimParameterModel GetModel(this AnimParameter instance)
        {
            AnimParameterModel model = new AnimParameterModel();

            model.Slider = instance.Slider.GetModel();
            model.BeatModifier = instance.BeatModifier.GetModel();

            return model;
        }

        public static void SetViewModel(this AnimParameter instance, AnimParameterModel model)
        {
            instance.Slider.SetViewModel(model.Slider);
            instance.BeatModifier.SetViewModel(model.BeatModifier);
        }
    }
}
