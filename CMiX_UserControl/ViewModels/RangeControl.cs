using System;
using CMiX.Services;
using CMiX.Models;

namespace CMiX.ViewModels
{
    public class RangeControl : ViewModel, IMessengerData
    {
        public RangeControl(IMessenger messenger, string layername)
            : this(
                  range: 0.0,
                  messenger: messenger,
                  messageEnabled : true,
                  messageaddress: String.Format("{0}/", layername),
                  modifier: ((RangeModifier)0).ToString()
                  )
        { }

        public RangeControl(
            double range,
            string messageaddress,
            bool messageEnabled,
            IMessenger messenger,
            string modifier)
        {
            AssertNotNegative(() => range);
            MessageAddress = messageaddress;
            MessageEnabled = messageEnabled;
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
                if(MessageEnabled)
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
                if(MessageEnabled)
                    Messenger.SendMessage(MessageAddress + nameof(Modifier), Modifier);
            }
        }

        public void Copy(RangeControlDTO rangecontroldto)
        {
            rangecontroldto.Range = Range;
            rangecontroldto.Modifier = Modifier;
        }

        public void Paste(RangeControlDTO rangecontroldto)
        {
            MessageEnabled = false;

            Range = rangecontroldto.Range;
            Modifier = rangecontroldto.Modifier;

            MessageEnabled = true;
        }
    }
}