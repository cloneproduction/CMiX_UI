using System;
using System.Windows.Media;
using CMiX.Services;

namespace CMiX.ViewModels
{
    public class Coloration : ViewModel
    {
        public Coloration(Beat masterbeat, string layerName, IMessenger messenger)
            : this(
                  beatModifier: new BeatModifier(masterbeat, layerName + "/" + nameof(Coloration), messenger),
                  layerName: layerName,
                  messenger: messenger,
                  objectColor: Colors.White,
                  backgroundColor: Colors.Black,
                  hue: new HSVPoint(),
                  saturation: new HSVPoint(),
                  value: new HSVPoint())
        { }

        public Coloration(
            BeatModifier beatModifier, 
            string layerName, 
            Color objectColor, 
            Color backgroundColor, 
            HSVPoint hue, 
            HSVPoint saturation, 
            HSVPoint value, 
            IMessenger messenger)
        {
            LayerName = layerName;
            BeatModifier = beatModifier ?? throw new ArgumentNullException(nameof(beatModifier));
            ObjectColor = objectColor;
            Messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
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

        private IMessenger Messenger { get; }

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
                    Messenger.SendMessage(LayerName, ObjectColor.ToString());
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
