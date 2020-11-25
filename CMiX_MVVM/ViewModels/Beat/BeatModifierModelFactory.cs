using CMiX.MVVM.Models;

namespace CMiX.MVVM.ViewModels
{
    public static class BeatModifierModelFactory
    {
        public static BeatModifierModel GetModel(this BeatModifier instance)
        {
            BeatModifierModel model = new BeatModifierModel();
            //model.BeatIndex = instance.BeatIndex;
            //model.ChanceToHit = instance.ChanceToHit.GetModel();
            //model.Multiplier = instance.Multiplier;
            return model;
        }

        public static void SetViewModel(this BeatModifier instance, BeatModifierModel model)
        {
            instance.BeatIndex = model.BeatIndex;
            instance.Multiplier = model.Multiplier;
            instance.ChanceToHit.SetViewModel(model.ChanceToHit);
        }
    }
}