using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Mediator;

namespace CMiX.MVVM.ViewModels
{
    public class Texture : Sender, IColleague
    {
        public Texture(string name, IColleague parentSender)
        {
            this.Address = $"{parentSender.Address}{name}/";
            this.MessageMediator = parentSender.MessageMediator;
            this.MessageMediator.RegisterColleague(this);

            AssetPathSelector = new AssetPathSelector(new AssetTexture(), this);

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
        public AssetTexture AssetTexture { get; set; }

        public Slider Brightness { get; }
        public Slider Contrast { get; }
        public Slider Hue { get; }
        public Slider Saturation { get; }
        public Slider Luminosity { get; }
        public Slider Keying { get; }
        public Slider Pan { get; }
        public Slider Tilt { get; }
        public Slider Scale { get; }
        public Slider Rotate { get; }
        public Inverter Inverter { get; set; }
        public MessageMediator MessageMediator { get; set; }

        public void Send(Message message)
        {
            MessageMediator?.Notify(MessageDirection.OUT, message);
        }

        public void Receive(Message message)
        {
            this.SetViewModel(message.Obj as TextureModel);
            System.Console.WriteLine("POUETPOUET " + this.Address + "Texture received " + message.Address);
        }
    }
}