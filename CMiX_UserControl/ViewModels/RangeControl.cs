using System;
using CMiX.Services;

namespace CMiX.ViewModels
{
    public class RangeControl : ViewModel, IMessengerData
    {
        public RangeControl(IMessenger messenger, string layername)
            : this(
                  range: 0.0,
                  messenger: messenger,
                  messageaddress: String.Format("{0}/{1}/", layername, nameof(RangeControl)),
                  modifier: ((RangeModifier)0).ToString()
                  )
        { }

        public RangeControl(
            double range,
            string messageaddress,
            IMessenger messenger,
            string modifier)
        {
            AssertNotNegative(() => range);
            MessageAddress = messageaddress;
            Messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
            Range = range;
            Modifier = modifier;
        }

        public string MessageAddress { get; set; }

        public bool MessageEnabled { get; set; }

        private IMessenger Messenger { get; }

        private double _range;
        [OSC]
        public double Range
        {
            get => _range;
            set
            {
                SetAndNotify(ref _range, CoerceNotNegative(value));
                Messenger.SendMessage(MessageAddress + nameof(Range), Range);
            }
        }

        private string _modifier;
        [OSC]
        public string Modifier
        {
            get => _modifier;
            set
            {
                SetAndNotify(ref _modifier, value);
                Messenger.SendMessage(MessageAddress + nameof(Modifier), Modifier);
            }
        }
    }
}