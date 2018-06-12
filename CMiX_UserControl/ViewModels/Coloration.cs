using System;
using System.Windows.Media;

namespace CMiX.ViewModels
{
    public class Coloration : ViewModel
    {
        public Coloration(Beat masterbeat, string layername)
            : this(
                  beatModifier: new BeatModifier(masterbeat, layername, nameof(Coloration)),
                  message: new Messenger(),
                  objectColor: Colors.White,
                  backgroundColor: Colors.Black,
                  hue: new HSVPoint(),
                  saturation: new HSVPoint(),
                  value: new HSVPoint())
        { }

        public Coloration(BeatModifier beatModifier, Messenger message, Color objectColor, Color backgroundColor, HSVPoint hue, HSVPoint saturation, HSVPoint value)
        {
            BeatModifier = beatModifier ?? throw new ArgumentNullException(nameof(beatModifier));
            ObjectColor = objectColor;
            Message = message;
            BackgroundColor = backgroundColor;
            Hue = hue ?? throw new ArgumentNullException(nameof(hue));
            Saturation = saturation ?? throw new ArgumentNullException(nameof(saturation));
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        private string _layerName;
        public string LayerName
        {
            get => _layerName;
            set => SetAndNotify(ref _layerName, value);
        }

        private Messenger _message;
        public Messenger Message
        {
            get => _message;
            set => SetAndNotify(ref _message, value);
        }

        private Color _objectColor;
        public Color ObjectColor
        {
            get => _objectColor;
            set
            {
                SetAndNotify(ref _objectColor, value);
                //problem with IF, sometimes NULL
                if(Value != null)
                {
                    Message.SendOSC("pouet", ObjectColor.ToString());
                }

            }
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
