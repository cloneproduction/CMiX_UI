using System;
using System.Windows.Media;
using CMiX.Services;

namespace CMiX.ViewModels
{
    public class Coloration : ViewModel
    {
        public Coloration(Beat masterbeat, string layerName, IMessenger messenger)
            : this(
                  messenger: messenger,
                  beatModifier: new BeatModifier(masterbeat, layerName + "/" + nameof(Coloration), messenger),
                  layerName: layerName,
                  objColor: Colors.BlueViolet,
                  bgColor: Colors.Black,
                  backgroundColor: Colors.Black,
                  hue: new RangeControl(messenger, layerName + "/" + nameof(Coloration) + "/" + nameof(Hue)),
                  saturation: new RangeControl(messenger, layerName + "/" + nameof(Coloration) + "/" + nameof(Saturation)),
                  value: new RangeControl(messenger, layerName + "/" + nameof(Coloration) + "/" + nameof(Value))
                  )
        { }

        public Coloration(
            IMessenger messenger,
            BeatModifier beatModifier, 
            string layerName, 
            Color objColor,
            Color bgColor,
            Color backgroundColor,
            RangeControl hue,
            RangeControl saturation,
            RangeControl value)
        {
            Messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
            LayerName = layerName;
            BeatModifier = beatModifier ?? throw new ArgumentNullException(nameof(beatModifier));
            ObjColor = objColor;
            BgColor = bgColor;
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
                Messenger.SendMessage(LayerName + "/" + nameof(Coloration) + "/" + nameof(ObjColor), ObjColor);
            }
        }

        private Color _bgColor;
        public Color BgColor
        {
            get => _bgColor;
            set
            {
                SetAndNotify(ref _bgColor, value);
                Messenger.SendMessage(LayerName + "/" + nameof(Coloration) + "/" + nameof(BgColor), BgColor);
            }
        }

        public BeatModifier BeatModifier { get; }

        public RangeControl Hue { get; }

        public RangeControl Saturation { get; }

        public RangeControl Value { get; }
    }
}
