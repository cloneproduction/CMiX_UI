using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.Assets;
using CMiX.MVVM.ViewModels.Beat;

namespace CMiX.MVVM.ViewModels
{
    public class Texture : Control
    {
        public Texture(MasterBeat beat, TextureModel textureModel)
        {
            AssetPathSelector = new AssetPathSelector(new AssetTexture(), textureModel.AssetPathSelectorModel);
            Inverter = new Inverter(nameof(Inverter), textureModel.InverterModel);

            Brightness = new Slider(nameof(Brightness), textureModel.Brightness) { Minimum = -1.0, Maximum = 1.0 };
            Contrast = new Slider(nameof(Contrast), textureModel.Contrast) { Minimum = -1.0, Maximum = 1.0 };
            Hue = new Slider(nameof(Hue), textureModel.Hue) { Minimum = -1.0, Maximum = 1.0 };
            Saturation = new Slider(nameof(Saturation), textureModel.Saturation) { Minimum = -1.0, Maximum = 1.0 };
            Luminosity = new Slider(nameof(Luminosity), textureModel.Luminosity) { Minimum = -1.0, Maximum = 1.0 };
            Keying = new Slider(nameof(Keying), textureModel.Keying);
            Scale = new Slider(nameof(Scale), textureModel.Scale) { Minimum = -1.0, Maximum = 1.0 };
            Rotate = new Slider(nameof(Rotate), textureModel.Rotate) { Minimum = -1.0, Maximum = 1.0 };
            Pan = new Slider(nameof(Pan), textureModel.Pan) { Minimum = -1.0, Maximum = 1.0 };
            Tilt = new Slider(nameof(Tilt), textureModel.Tilt) { Minimum = -1.0, Maximum = 1.0 };
        }


        public AssetPathSelector AssetPathSelector { get; set; }
        public Slider Brightness { get; set; }
        public Slider Contrast { get; set; }
        public Slider Hue { get; set; }
        public Slider Saturation { get; set; }
        public Slider Luminosity { get; set; }
        public Slider Keying { get; set; }
        public Slider Pan { get; set; }
        public Slider Tilt { get; set; }
        public Slider Scale { get; set; }
        public Slider Rotate { get; set; }
        public Inverter Inverter { get; set; }


        //public override void SetReceiver(IMessageReceiver messageReceiver)
        //{
        //    Brightness.SetReceiver(messageReceiver);
        //    Contrast.SetReceiver(messageReceiver);
        //    Hue.SetReceiver(messageReceiver);
        //    Saturation.SetReceiver(messageReceiver);
        //    Luminosity.SetReceiver(messageReceiver);
        //    Keying.SetReceiver(messageReceiver);
        //    Scale.SetReceiver(messageReceiver);
        //    Rotate.SetReceiver(messageReceiver);
        //    Pan.SetReceiver(messageReceiver);
        //    Tilt.SetReceiver(messageReceiver);
        //}


        public override IModel GetModel()
        {
            TextureModel model = new TextureModel();
            model.AssetPathSelectorModel = (AssetPathSelectorModel)this.AssetPathSelector.GetModel();
            model.InverterModel = (InverterModel)this.Inverter.GetModel();
            model.Brightness = (SliderModel)this.Brightness.GetModel();
            model.Contrast = (SliderModel)this.Contrast.GetModel();
            model.Saturation = (SliderModel)this.Saturation.GetModel();
            model.Luminosity = (SliderModel)this.Luminosity.GetModel();
            model.Hue = (SliderModel)this.Hue.GetModel();
            model.Pan = (SliderModel)this.Pan.GetModel();
            model.Tilt = (SliderModel)this.Tilt.GetModel();
            model.Scale = (SliderModel)this.Scale.GetModel();
            model.Rotate = (SliderModel)this.Rotate.GetModel();
            model.Keying = (SliderModel)this.Keying.GetModel();
            return model;
        }


        public override void SetViewModel(IModel model)
        {
            TextureModel textureModel = model as TextureModel;
            this.AssetPathSelector.SetViewModel(textureModel.AssetPathSelectorModel);
            this.Inverter.SetViewModel(textureModel.InverterModel);
            this.Brightness.SetViewModel(textureModel.Brightness);
            this.Contrast.SetViewModel(textureModel.Contrast);
            this.Saturation.SetViewModel(textureModel.Saturation);
            this.Luminosity.SetViewModel(textureModel.Luminosity);
            this.Hue.SetViewModel(textureModel.Hue);
            this.Pan.SetViewModel(textureModel.Pan);
            this.Tilt.SetViewModel(textureModel.Tilt);
            this.Scale.SetViewModel(textureModel.Scale);
            this.Rotate.SetViewModel(textureModel.Rotate);
            this.Keying.SetViewModel(textureModel.Keying);
        }
    }
}