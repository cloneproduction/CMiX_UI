using System.Windows.Media;

namespace CMiX.ViewModels
{
    public class Coloration : ViewModel
    {
        Color _ObjectColor;
        public Color ObjectColor
        {
            get => _ObjectColor;
            set => SetAndNotify(ref _ObjectColor, value);
        }

        Color _BackgroundColor;
        public Color BackgroundColor
        {
            get => _BackgroundColor;
            set => SetAndNotify(ref _BackgroundColor, value);
        }

        double _HueColor = 0.0;
        public double HueColor
        {
            get => _HueColor;
            set => SetAndNotify(ref _HueColor, value);
        }

        ColorationModifier _HueModifier;
        public ColorationModifier HueModifier
        {
            get => _HueModifier;
            set => SetAndNotify(ref _HueModifier, value);
        }

        double _Saturation = 0.0;
        public double Saturation
        {
            get => _Saturation;
            set => SetAndNotify(ref _Saturation, value);
        }

        ColorationModifier _SaturationModifier;
        public ColorationModifier SaturationModifier
        {
            get => _SaturationModifier;
            set => SetAndNotify(ref _SaturationModifier, value);
        }

        double _Lightness = 0.0;
        public double Lightness
        {
            get => _Lightness;
            set => SetAndNotify(ref _Lightness, value);
        }

        ColorationModifier _LightnessModifier;
        public ColorationModifier LightnessModifier
        {
            get => _LightnessModifier;
            set => SetAndNotify(ref _LightnessModifier, value);
        }
    }
}
