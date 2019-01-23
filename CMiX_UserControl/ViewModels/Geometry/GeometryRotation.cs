using System;
using CMiX.Services;
using CMiX.Models;
using System.Collections.Generic;
using GuiLabs.Undo;

namespace CMiX.ViewModels
{
    public class GeometryRotation :ViewModel, IMessengerData
    {
        public GeometryRotation(string layername, IMessenger messenger, ActionManager actionmanager)
            : this
            (
                actionmanager: actionmanager,
                messageaddress: layername + "/",
                messenger: messenger,
                messageEnabled: true,
                rotationMode: default,
                rotationX: true,
                rotationY: true,
                rotationZ: true
            )
        {
        }

        public GeometryRotation
            (
                ActionManager actionmanager,
                string messageaddress,
                bool rotationX,
                bool rotationY,
                bool rotationZ,
                bool messageEnabled,
                IMessenger messenger,
                GeometryRotationMode rotationMode
            )
            : base (actionmanager)
        {
            MessageAddress = messageaddress;
            MessageEnabled = messageEnabled;
            Messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
            RotationX = rotationX;
            RotationY = rotationY;
            RotationZ = rotationZ;
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

        private bool _RotationX;
        [OSC]
        public bool RotationX
        {
            get => _RotationX;
            set
            {
                SetAndNotify(ref _RotationX, value);
                if (MessageEnabled)
                    Messenger.SendMessage(MessageAddress + nameof(RotationX), RotationX);
            }
        }

        private bool _RotationY;
        [OSC]
        public bool RotationY
        {
            get => _RotationY;
            set
            {
                SetAndNotify(ref _RotationY, value);
                if (MessageEnabled)
                    Messenger.SendMessage(MessageAddress + nameof(RotationY), RotationY);
            }
        }

        private bool _RotationZ;
        [OSC]
        public bool RotationZ
        {
            get => _RotationZ;
            set
            {
                SetAndNotify(ref _RotationZ, value);
                if (MessageEnabled)
                    Messenger.SendMessage(MessageAddress + nameof(RotationZ), RotationZ);
            }
        }

        public void Copy(GeometryRotationDTO geometryrotationdto)
        {
            geometryrotationdto.RotationModeDTO = RotationMode;
            geometryrotationdto.RotationX = RotationX;
            geometryrotationdto.RotationY = RotationY;
            geometryrotationdto.RotationZ = RotationZ;
        }

        public void Paste(GeometryRotationDTO geometryrotationdto)
        {
            MessageEnabled = false;
            RotationMode = geometryrotationdto.RotationModeDTO;
            RotationX = geometryrotationdto.RotationX;
            RotationY = geometryrotationdto.RotationY;
            RotationZ = geometryrotationdto.RotationZ;
            MessageEnabled = true;
        }
    }
}