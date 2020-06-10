using CMiX.MVVM.Models;

namespace CMiX.MVVM.ViewModels
{
    public static class ColorationModelFactory
    {
        public static ColorationModel GetModel(this Coloration instance)
        {
            ColorationModel colorationModel = new ColorationModel();
            colorationModel.ColorSelectorModel = instance.ColorSelector.GetModel();
            //colorationModel.BeatModifierModel = instance.BeatModifier.GetModel();
            //colorationModel.HueModel = instance.Hue.GetModel();
            //colorationModel.SatModel = instance.Saturation.GetModel();
            //colorationModel.ValModel = instance.Value.GetModel();
            return colorationModel;
        }

        public static void SetViewModel(this Coloration instance, ColorationModel colorationModel)
        {
            instance.ColorSelector.SetViewModel(colorationModel.ColorSelectorModel);
            //instance.BeatModifier.SetViewModel(colorationModel.BeatModifierModel);
            //instance.Hue.SetViewModel(colorationModel.HueModel);
            //instance.Saturation.SetViewModel(colorationModel.SatModel);
            //instance.Value.SetViewModel(colorationModel.ValModel);
        }
    }
}