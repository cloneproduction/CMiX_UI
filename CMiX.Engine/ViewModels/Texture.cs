using CMiX.MVVM;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using System;

namespace CMiX.Engine.ViewModels
{
    public class Texture : IMessageReceiver, ICopyPasteModel<TextureModel>
    {
        public Texture(Receiver receiver, string messageAddress)
        {
            MessageAddress = $"{messageAddress}{nameof(Texture)}/";
            Receiver = receiver;
            Receiver.MessageReceived += OnMessageReceived;

            new Slider(receiver, MessageAddress + nameof(Brightness));
            new Slider(receiver, MessageAddress + nameof(Contrast));
            new Slider(receiver, MessageAddress + nameof(Invert));
            new Slider(receiver, MessageAddress + nameof(Hue));
            new Slider(receiver, MessageAddress + nameof(Saturation));
            new Slider(receiver, MessageAddress + nameof(Luminosity));
            new Slider(receiver, MessageAddress + nameof(Keying));
            new Slider(receiver, MessageAddress + nameof(Pan));
            new Slider(receiver, MessageAddress + nameof(Tilt));
            new Slider(receiver, MessageAddress + nameof(Scale));
            new Slider(receiver, MessageAddress + nameof(Rotate));
        }

        public string MessageAddress { get; set; }
        public Receiver Receiver { get; set; }

        public Slider Brightness { get; set; }
        public Slider Contrast { get; set; }
        public Slider Invert { get; set; }
        public Slider Hue { get; set; }
        public Slider Saturation { get; set; }
        public Slider Luminosity { get; set; }
        public Slider Keying { get; set; }
        public Slider Pan { get; set; }
        public Slider Tilt { get; set; }
        public Slider Scale { get; set; }
        public Slider Rotate { get; set; }

        public void OnMessageReceived(object sender, EventArgs e)
        {
            Receiver.UpdateViewModel(MessageAddress, this);
        }

        public void PasteModel(TextureModel textureModel)
        {

        }

        public void CopyModel(TextureModel textureModel)
        {
            throw new NotImplementedException();
        }
    }
}