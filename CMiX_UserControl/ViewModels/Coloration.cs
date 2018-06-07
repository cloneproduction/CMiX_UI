using System;
using System.Windows.Media;

namespace CMiX.ViewModels
{
    public class Coloration : ViewModel
    {
        public Coloration(Beat masterBeat)
            : this(
                  masterBeat: new BeatModifier(masterBeat),
                  objectColor: Colors.White,
                  backgroundColor: Colors.Black,
                  hue: new HSVPoint(),
                  saturation: new HSVPoint(),
                  value: new HSVPoint())
        { }

        public Coloration(Beat masterBeat, Color objectColor, Color backgroundColor, HSVPoint hue, HSVPoint saturation, HSVPoint value)
        {
            MasterBeat = masterBeat ?? throw new ArgumentNullException(nameof(masterBeat));
            ObjectColor = objectColor;
            BackgroundColor = backgroundColor;
            Hue = hue ?? throw new ArgumentNullException(nameof(hue));
            Saturation = saturation ?? throw new ArgumentNullException(nameof(saturation));
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        private Color _objectColor;

        public Beat MasterBeat { get; }
        public Color ObjectColor
        {
            get => _objectColor;
            set => SetAndNotify(ref _objectColor, value);
        }

        private Color _backgroundColor;
        public Color BackgroundColor
        {
            get => _backgroundColor;
            set => SetAndNotify(ref _backgroundColor, value);
        }

        public BeatModifier BeatModifier { get; }

        public HSVPoint Hue { get; }

        public HSVPoint Saturation { get; }

        public HSVPoint Value { get; }
    }
}
