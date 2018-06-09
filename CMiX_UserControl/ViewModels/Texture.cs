using System;
using System.Collections.ObjectModel;

namespace CMiX.ViewModels
{
    public class Texture : ViewModel
    {
        public Texture(string layername, string containername)
            : this(
                  layerName: layername,
                  containerName: containername,
                  message: new Messenger(),
                  brightness: 0.0,
                  contrast: 0.0,
                  saturation: 0.0,
                  keying: 0.0,
                  invert: 0.0,
                  invertMode: default(TextureInvertMode))
        { }

        public Texture(
            string layerName,
            string containerName,
            Messenger message,
            double brightness,
            double contrast,
            double saturation,
            double keying,
            double invert,
            TextureInvertMode invertMode)
        {
            AssertNotNegative(() => brightness);
            AssertNotNegative(() => contrast);
            AssertNotNegative(() => saturation);
            AssertNotNegative(() => keying);
            AssertNotNegative(() => invert);

            LayerName = layerName;
            ContainerName = containerName;
            Message = message;
            Brightness = brightness;
            Contrast = contrast;
            Saturation = saturation;
            Keying = keying;
            Invert = invert;
            InvertMode = invertMode;
        }

        private string _layerName;
        public string LayerName
        {
            get => _layerName;
            set => SetAndNotify(ref _layerName, value);
        }

        private string _containerName;
        public string ContainerName
        {
            get => _containerName;
            set => SetAndNotify(ref _containerName, value);
        }

        private Messenger _message;
        public Messenger Message
        {
            get => _message;
            set => SetAndNotify(ref _message, value);
        }

        private string Address => String.Format("{0}/{1}/{2}/", LayerName, ContainerName, nameof(Texture));

        public ObservableCollection<string> SelectedTexturePaths { get; }

        private double _brightness;
        public double Brightness
        {
            get => _brightness;
            set
            {
                SetAndNotify(ref _brightness, CoerceNotNegative(value));
                Message.SendOSC(Address + nameof(Brightness), Brightness.ToString());
            }
        }

        private double _contrast;
        public double Contrast
        {
            get => _contrast;
            set
            {
                SetAndNotify(ref _contrast, CoerceNotNegative(value));
                Message.SendOSC(Address + nameof(Contrast), Contrast.ToString());
            }
        }

        private double _saturation;
        public double Saturation
        {
            get => _saturation;
            set
            {
                SetAndNotify(ref _saturation, CoerceNotNegative(value));
                Message.SendOSC(Address + nameof(Saturation), Saturation.ToString());
            }
        }

        private double _keying;
        public double Keying
        {
            get => _keying;
            set
            {
                SetAndNotify(ref _keying, CoerceNotNegative(value));
                Message.SendOSC(Address + nameof(Keying), Keying.ToString());
            }
        }

        private double _invert;
        public double Invert
        {
            get => _invert;
            set
            {
                SetAndNotify(ref _invert, CoerceNotNegative(value));
                Message.SendOSC(Address + nameof(Invert), Invert.ToString());
            }
        }

        private TextureInvertMode _invertMode;
        public TextureInvertMode InvertMode
        {
            get => _invertMode;
            set => SetAndNotify(ref _invertMode, value);
        }
    }
}
