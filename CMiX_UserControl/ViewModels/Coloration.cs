using System;
using System.Windows.Media;
using CMiX.Services;

namespace CMiX.ViewModels
{
    [Serializable]
    public class Coloration : ViewModel, IMessengerData
    {
        public Coloration(Beat masterbeat, string layername, IMessenger messenger)
            : this(
                  messenger: messenger,
                  messageaddress: String.Format("{0}/{1}/", layername, nameof(Coloration)),
                  beatModifier: new BeatModifier(String.Format("{0}/{1}", layername, nameof(Coloration)), messenger, masterbeat),
                  objColor: Colors.BlueViolet,
                  bgColor: Colors.Black,
                  backgroundColor: Colors.Black,
                  hue: new RangeControl(messenger, String.Format("{0}/{1}", layername, nameof(Coloration)) + "/" + nameof(Hue)),
                  saturation: new RangeControl(messenger, String.Format("{0}/{1}", layername, nameof(Coloration)) + "/" + nameof(Saturation)),
                  value: new RangeControl(messenger, String.Format("{0}/{1}", layername, nameof(Coloration)) + "/" + nameof(Value))
                  )
        { }

        public Coloration(
            IMessenger messenger,
            string messageaddress,
            BeatModifier beatModifier, 
            Color objColor,
            Color bgColor,
            Color backgroundColor,
            RangeControl hue,
            RangeControl saturation,
            RangeControl value)
        {
            Messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
            MessageAddress = messageaddress;
            BeatModifier = beatModifier ?? throw new ArgumentNullException(nameof(beatModifier));
            ObjColor = objColor;
            BgColor = bgColor;
            Hue = hue ?? throw new ArgumentNullException(nameof(hue));
            Saturation = saturation ?? throw new ArgumentNullException(nameof(saturation));
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }
        private IMessenger Messenger { get; }

        public string MessageAddress { get; set; }

        public bool MessageEnabled { get; set; }

        private Color _objColor;
        [OSC]
        public Color ObjColor
        {
            get => _objColor;
            set
            {
                SetAndNotify(ref _objColor, value);
                if(MessageEnabled)
                    Messenger.SendMessage(MessageAddress + nameof(ObjColor), ObjColor);
            }
        }

        private Color _bgColor;
        [OSC]
        public Color BgColor
        {
            get => _bgColor;
            set
            {
                SetAndNotify(ref _bgColor, value);
                if(MessageEnabled)
                    Messenger.SendMessage(MessageAddress + nameof(BgColor), BgColor);
            }
        }

        public BeatModifier BeatModifier { get; }

        public RangeControl Hue { get; }

        public RangeControl Saturation { get; }

        public RangeControl Value { get; }
    }
}