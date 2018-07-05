using System;
using CMiX.Services;

namespace CMiX.ViewModels
{
    public class GeometryRotation :ViewModel, IMessengerData
    {
        public GeometryRotation(string layername, IMessenger messenger)
            : this(
                  messageaddress: String.Format("{0}/{1}", layername, nameof(RotationMode)),
                  messenger: messenger,
                  messageEnabled : true,
                  rotationMode: default
                  )
        {
        }

        public GeometryRotation
            (
                string messageaddress,
                bool messageEnabled,
                IMessenger messenger,
                GeometryRotationMode rotationMode
            )
        {
            MessageAddress = messageaddress;
            MessageEnabled = messageEnabled;
            Messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
        }

        public string MessageAddress { get; set; }

        public bool MessageEnabled { get; set; }

        public IMessenger Messenger { get; }

        private GeometryRotationMode _RotationMode;
        [OSC]
        public GeometryRotationMode RotationMode
        {
            get => _RotationMode;
            set
            {
                SetAndNotify(ref _RotationMode, value);
                if(MessageEnabled)
                    Messenger.SendMessage(MessageAddress, RotationMode);
            }
        }
    }
}