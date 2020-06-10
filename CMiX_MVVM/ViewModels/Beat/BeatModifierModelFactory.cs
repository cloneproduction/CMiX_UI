using CMiX.MVVM.Models;

namespace CMiX.MVVM.ViewModels
{
    public static class BeatModifierModelFactory
    {
        public static BeatModifierModel GetModel(this BeatModifier instance)
        {
            BeatModifierModel beatModifierModel = new BeatModifierModel();
            beatModifierModel.ChanceToHit = instance.ChanceToHit.GetModel();
            beatModifierModel.Multiplier = instance.Multiplier;
            return beatModifierModel;
        }

        public static void SetViewModel(this BeatModifier instance, BeatModifierModel beatModifierModel)
        {
            instance.Multiplier = beatModifierModel.Multiplier;
            instance.ChanceToHit.SetViewModel(beatModifierModel.ChanceToHit);
        }
    }
}