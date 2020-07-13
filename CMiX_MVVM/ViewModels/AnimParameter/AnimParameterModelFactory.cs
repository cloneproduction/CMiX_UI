using CMiX.MVVM.Models;

namespace CMiX.MVVM.ViewModels
{
    public static class AnimParameterModelFactory
    {
        public static AnimParameterModel GetModel(this AnimParameter instance)
        {
            AnimParameterModel model = new AnimParameterModel();
           // model.Mode = instance.Mode.GetModel();
           // model.Influence = instance.Influence.GetModel();
            model.BeatModifier = instance.BeatModifier.GetModel();
            return model;
        }

        public static void SetViewModel(this AnimParameter instance, AnimParameterModel model)
        {
            //instance.Mode.SetViewModel(model.Mode);
           // instance.Influence.SetViewModel(model.Influence);
            instance.BeatModifier.SetViewModel(model.BeatModifier);
        }
    }
}
