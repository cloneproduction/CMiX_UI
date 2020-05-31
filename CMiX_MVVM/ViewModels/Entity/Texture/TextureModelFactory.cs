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
            model.Brightness = instance.Brightness.GetModel();
            model.Contrast = instance.Contrast.GetModel();
            model.Saturation = instance.Saturation.GetModel();
            model.Luminosity = instance.Luminosity.GetModel();
            model.Hue = instance.Hue.GetModel();
            model.Pan = instance.Pan.GetModel();
            model.Tilt = instance.Tilt.GetModel();
            model.Scale = instance.Scale.GetModel();
            model.Rotate = instance.Rotate.GetModel();
            model.Keying = instance.Keying.GetModel();
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
