using System;
using System.Collections.ObjectModel;
using CMiX.Services;
using CMiX.Controls;
using System.Collections.Generic;

namespace CMiX.ViewModels
{
    public class Texture : ViewModel
    {
        public Texture(string layername, IMessenger messenger)
            : this(
                  layerName: layername,
                  messenger: messenger,
                  texturePaths : new ObservableCollection<ListBoxFileName>(),
                  brightness: 0.0,
                  contrast: 0.0,
                  saturation: 0.0,
                  keying: 0.0,
                  invert: 0.0,
                  invertMode: default(TextureInvertMode))
        { }

        public Texture(
            string layerName,
            IEnumerable<ListBoxFileName> texturePaths,
            IMessenger messenger,
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
            TexturePaths = new ObservableCollection<ListBoxFileName>();
            Messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
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

        public IMessenger Messenger { get; }

        private string Address => String.Format("{0}/{1}/", LayerName, nameof(Texture));

        public ObservableCollection<ListBoxFileName> TexturePaths { get; }

        private double _brightness;
        public double Brightness
        {
            get => _brightness;
            set
            {
                SetAndNotify(ref _brightness, CoerceNotNegative(value));
                Messenger.SendMessage(Address + nameof(Brightness), Brightness);
            }
        }

        private double _contrast;
        public double Contrast
        {
            get => _contrast;
            set
            {
                SetAndNotify(ref _contrast, CoerceNotNegative(value));
                Messenger.SendMessage(Address + nameof(Contrast), Contrast);
            }
        }

        private double _saturation;
        public double Saturation
        {
            get => _saturation;
            set
            {
                SetAndNotify(ref _saturation, CoerceNotNegative(value));
                Messenger.SendMessage(Address + nameof(Saturation), Saturation);
            }
        }

        private double _keying;
        public double Keying
        {
            get => _keying;
            set
            {
                SetAndNotify(ref _keying, CoerceNotNegative(value));
                Messenger.SendMessage(Address + nameof(Keying), Keying);
            }
        }

        private double _invert;
        public double Invert
        {
            get => _invert;
            set
            {
                SetAndNotify(ref _invert, CoerceNotNegative(value));
                Messenger.SendMessage(Address + nameof(Invert), Invert);
            }
        }

        private TextureInvertMode _invertMode;
        public TextureInvertMode InvertMode
        {
            get => _invertMode;
            set
            {
                SetAndNotify(ref _invertMode, value);
                Messenger.SendMessage(Address + nameof(InvertMode), InvertMode);
            }
        }
    }
}
