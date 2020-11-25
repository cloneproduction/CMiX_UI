using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Mediator;

namespace CMiX.MVVM.ViewModels
{
    public class Texture : Sender
    {
        public Texture(string name, IColleague parentSender) : base (name, parentSender)
        {
            AssetPathSelector = new AssetPathSelector(nameof(AssetPathSelector), this, new AssetTexture());

            Inverter = new Inverter(nameof(Inverter), this);

            Brightness = new Slider(nameof(Brightness), this);
            Brightness.Minimum = -1.0;

            Contrast = new Slider(nameof(Contrast), this);
            Contrast.Minimum = -1.0;

            Hue = new Slider(nameof(Hue), this);
            Hue.Minimum = -1.0;

            Saturation = new Slider(nameof(Saturation), this);
            Saturation.Minimum = -1.0;

            Luminosity = new Slider(nameof(Luminosity), this);
            Luminosity.Minimum = -1.0;

            Keying = new Slider(nameof(Keying), this);

            Scale = new Slider(nameof(Scale), this);
            Scale.Minimum = -1.0;

            Rotate = new Slider(nameof(Rotate), this);
            Rotate.Minimum = -1.0;

            Pan = new Slider(nameof(Pan), this);
            Pan.Minimum = -1.0;

            Tilt = new Slider(nameof(Tilt), this);
            Tilt.Minimum = -1.0;
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

        public override void Receive(Message message)
        {
            this.SetViewModel(message.Obj as TextureModel);
            System.Console.WriteLine("POUETPOUET " + this.Address + "Texture received " + message.Address);
        }
    }
}