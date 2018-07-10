using System;
using CMiX.Services;
using CMiX.Models;

namespace CMiX.ViewModels
{
    public class GeometryRotation :ViewModel, IMessengerData
    {
        public GeometryRotation(string layername, IMessenger messenger)
            : this(
                  messageaddress: layername + "/",
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
                    Messenger.SendMessage(MessageAddress + nameof(RotationMode), RotationMode);
            }
        }

        public void Copy(GeometryRotationDTO geometryrotationdto)
        {
            geometryrotationdto.RotationModeDTO = RotationMode;
        }

        public void Paste(GeometryRotationDTO geometryrotationdto)
        {
            RotationMode = geometryrotationdto.RotationModeDTO;
        }
    }
}