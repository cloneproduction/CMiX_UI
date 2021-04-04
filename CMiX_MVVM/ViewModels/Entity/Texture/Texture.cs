using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.Assets;
using CMiX.MVVM.ViewModels.Beat;
using CMiX.MVVM.ViewModels.MessageService;

namespace CMiX.MVVM.ViewModels
{
    public class Texture : MessageCommunicator
    {
        public Texture(IMessageDispatcher messageDispatcher, MasterBeat beat, TextureModel textureModel)
        {
            AssetPathSelector = new AssetPathSelector(messageDispatcher, new AssetTexture(), textureModel.AssetPathSelectorModel);
            Inverter = new Inverter(nameof(Inverter), messageDispatcher, textureModel.InverterModel);

            //Brightness = new Slider(nameof(Brightness), messageDispatcher, textureModel.Brightness) { Minimum = -1.0, Maximum = 1.0 };
            //Contrast = new Slider(nameof(Contrast), messageDispatcher, textureModel.Contrast) { Minimum = -1.0, Maximum = 1.0 };
            //Hue = new Slider(nameof(Hue), messageDispatcher, textureModel.Hue) { Minimum = -1.0, Maximum = 1.0 };
            //Saturation = new Slider(nameof(Saturation), messageDispatcher, textureModel.Saturation) { Minimum = -1.0, Maximum = 1.0 };
            //Luminosity = new Slider(nameof(Luminosity), messageDispatcher, textureModel.Luminosity) { Minimum = -1.0, Maximum = 1.0 };
            //Keying = new Slider(nameof(Keying), messageDispatcher, textureModel.Keying);
            //Scale = new Slider(nameof(Scale), messageDispatcher, textureModel.Scale) { Minimum = -1.0, Maximum = 1.0 };
            //Rotate = new Slider(nameof(Rotate), messageDispatcher, textureModel.Rotate) { Minimum = -1.0, Maximum = 1.0 };
            //Pan = new Slider(nameof(Pan), messageDispatcher, textureModel.Pan) { Minimum = -1.0, Maximum = 1.0 };
            //Tilt = new Slider(nameof(Tilt), messageDispatcher, textureModel.Tilt) { Minimum = -1.0, Maximum = 1.0 };
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