using System.Windows.Media;

namespace CMiX.ViewModels
{
    public class Coloration : ViewModel
    {
        public Coloration()
            : this(
                  objectColor: default,
                  backgroundColor: default,
                  hue: new HSVPoint(),
                  saturation: new HSVPoint(),
                  lightness: new HSVPoint())
        { }

        public Coloration(Color objectColor, Color backgroundColor, HSVPoint hue, HSVPoint saturation, HSVPoint lightness)
        {
            ObjectColor = objectColor;
            BackgroundColor = backgroundColor;
            Hue = hue;
            Saturation = saturation;
            Lightness = lightness;
        }

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

        public HSVPoint Hue { get; }

        public HSVPoint Saturation { get; }

        public HSVPoint Lightness { get; }
    }
}
