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
                  objColor: Colors.White,
                  bgColor: Colors.Black,
                  backgroundColor: Colors.Black,
                  hue: new RangeControl(messenger, layerName + "/" + nameof(Coloration) + "/" + nameof(Hue)),
                  saturation: new RangeControl(messenger, layerName + "/" + nameof(Coloration) + "/" + nameof(Saturation)),
                  value: new RangeControl(messenger, layerName + "/" + nameof(Coloration) + "/" + nameof(Value))
                  )
        { }

        public Coloration(
            BeatModifier beatModifier, 
            string layerName, 
            Color objColor,
            Color bgColor,
            Color backgroundColor,
            RangeControl hue,
            RangeControl saturation,
            RangeControl value, 
            IMessenger messenger)
        {
            LayerName = layerName;
            BeatModifier = beatModifier ?? throw new ArgumentNullException(nameof(beatModifier));
            BgColor = bgColor;
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

        private Color _objColor;
        public Color ObjColor
        {
            get => _objColor;
            set
            {
                SetAndNotify(ref _objColor, value);
                //problem with IF, sometimes NULL
                if(Value != null)
                {
                    Messenger.SendMessage(LayerName + "/" + nameof(Coloration) + "/" + nameof(ObjColor), ObjColor);
                }
            }
        }

        private Color _bgColor;
        public Color BgColor
        {
            get => _bgColor;
            set
            {
                SetAndNotify(ref _bgColor, value);
                //problem with IF, sometimes NULL
                if (Value != null)
                {
                    Messenger.SendMessage(LayerName + "/" + nameof(Coloration) + "/" + nameof(BgColor), BgColor);
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

        public RangeControl Hue { get; }

        public RangeControl Saturation { get; }

        public RangeControl Value { get; }
    }
}
