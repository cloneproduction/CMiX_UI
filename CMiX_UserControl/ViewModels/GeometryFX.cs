using CMiX.Models;
using CMiX.Services;
using System;

namespace CMiX.ViewModels
{
    public class GeometryFX : ViewModel, IMessengerData
    {
        public GeometryFX(string layername, IMessenger messenger)
            : this(
                  amount: 0.0,
                  messageaddress: String.Format("{0}/{1}/", layername, nameof(GeometryFX)),
                  messenger: messenger,
                  messageEnabled: true)
        { }

        public GeometryFX(
            double amount,
            IMessenger messenger,
            string messageaddress,
            bool messageEnabled
            )
        {
            AssertNotNegative(() => amount);

            Amount = amount;
            Messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
            MessageAddress = messageaddress;
            MessageEnabled = messageEnabled;
        }


        public string MessageAddress { get; set; }

        public bool MessageEnabled { get; set; }

        public IMessenger Messenger { get; }

        private double _Amount;
        [OSC]
        public double Amount
        {
            get => _Amount;
            set
            {
                SetAndNotify(ref _Amount, CoerceNotNegative(value));
                if (MessageEnabled)
                    Messenger.SendMessage(MessageAddress + nameof(Amount), Amount);
            }
        }

        public void Copy(GeometryFXDTO geometryFXdto)
        {
            geometryFXdto.Amount = Amount;
        }

        public void Paste(GeometryFXDTO geometryFXdto)
        {
            MessageEnabled = false;

            Amount = geometryFXdto.Amount;

            MessageEnabled = true;
        }
    }
}
