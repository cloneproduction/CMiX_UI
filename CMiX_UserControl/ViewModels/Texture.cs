using System.Collections.ObjectModel;

namespace CMiX.ViewModels
{
    public class Texture : ViewModel
    {
        public Texture()
            : this(
                  brightness: 0.0,
                  contrast: 0.0,
                  saturation: 0.0,
                  keying: 0.0,
                  invert: 0.0,
                  invertMode: default(TextureInvertMode))
        { }

        public Texture(
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

            Brightness = brightness;
            Contrast = contrast;
            Saturation = saturation;
            Keying = keying;
            Invert = invert;
            InvertMode = invertMode;
        }

        public ObservableCollection<string> SelectedTexturePaths { get; }

        private double _brightness;
        public double Brightness
        {
            get => _brightness;
            set => SetAndNotify(ref _brightness, CoerceNotNegative(value));
        }

        private double _contrast;
        public double Contrast
        {
            get => _contrast;
            set => SetAndNotify(ref _contrast, CoerceNotNegative(value));
        }

        private double _saturation;
        public double Saturation
        {
            get => _saturation;
            set => SetAndNotify(ref _saturation, CoerceNotNegative(value));
        }

        private double _keying;
        public double Keying
        {
            get => _keying;
            set => SetAndNotify(ref _keying, CoerceNotNegative(value));
        }

        private double _invert;
        public double Invert
        {
            get => _invert;
            set => SetAndNotify(ref _invert, CoerceNotNegative(value));
        }

        private TextureInvertMode _invertMode;
        public TextureInvertMode InvertMode
        {
            get => _invertMode;
            set => SetAndNotify(ref _invertMode, value);
        }
    }
}
