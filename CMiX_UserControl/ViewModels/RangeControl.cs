using System;
using CMiX.Services;
using CMiX.Models;
using GuiLabs.Undo;

namespace CMiX.ViewModels
{
    public class RangeControl : ViewModel, IMessengerData
    {
        public RangeControl(IMessenger messenger, string layername, ActionManager actionmanager)
            : this(
                    actionmanager: actionmanager,
                    range: 0.0,
                    modifier: ((RangeModifier)0).ToString(),
                    messenger: messenger,
                    messageaddress: String.Format("{0}/", layername),
                    messageEnabled: true
                  )
        { }

        public RangeControl
            (
                ActionManager actionmanager,
                double range,
                string modifier,
                IMessenger messenger,
                string messageaddress,
                bool messageEnabled
            )
            : base(actionmanager)
        {
            AssertNotNegative(() => range);
            Range = range;
            Modifier = modifier;
            Messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
            MessageAddress = messageaddress;
            MessageEnabled = messageEnabled;
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