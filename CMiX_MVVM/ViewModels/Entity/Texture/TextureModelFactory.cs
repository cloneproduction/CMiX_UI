using CMiX.MVVM.Models;

namespace CMiX.MVVM.ViewModels
{
    public static class TextureModelFactory
    {
        public static TextureModel GetModel(this Texture instance)
        {
            TextureModel model = new TextureModel();
            model.AssetPathSelectorModel = instance.AssetPathSelector.GetModel();
            model.InverterModel = instance.Inverter.GetModel();
            model.Brightness = (SliderModel)instance.Brightness.GetModel();
            model.Contrast = (SliderModel)instance.Contrast.GetModel();
            model.Saturation = (SliderModel)instance.Saturation.GetModel();
            model.Luminosity = (SliderModel)instance.Luminosity.GetModel();
            model.Hue = (SliderModel)instance.Hue.GetModel();
            model.Pan = (SliderModel)instance.Pan.GetModel();
            model.Tilt = (SliderModel)instance.Tilt.GetModel();
            model.Scale = (SliderModel)instance.Scale.GetModel();
            model.Rotate = (SliderModel)instance.Rotate.GetModel();
            model.Keying = (SliderModel)instance.Keying.GetModel();
            return model;
        }

        public static void SetViewModel(this Texture instance, TextureModel model)
        {
            instance.AssetPathSelector.SetViewModel(model.AssetPathSelectorModel);
            instance.Inverter.SetViewModel(model.InverterModel);
            instance.Brightness.SetViewModel(model.Brightness);
            instance.Contrast.SetViewModel(model.Contrast);
            instance.Saturation.SetViewModel(model.Saturation);
            instance.Luminosity.SetViewModel(model.Luminosity);
            instance.Hue.SetViewModel(model.Hue);
            instance.Pan.SetViewModel(model.Pan);
            instance.Tilt.SetViewModel(model.Tilt);
            instance.Scale.SetViewModel(model.Scale);
            instance.Rotate.SetViewModel(model.Rotate);
            instance.Keying.SetViewModel(model.Keying);
        }
    }
}
