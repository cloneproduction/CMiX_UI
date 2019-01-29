using System;
using CMiX.Services;
using CMiX.Models;
using GuiLabs.Undo;

namespace CMiX.ViewModels
{
    public class GeometryRotation :ViewModel
    {
        #region CONSTRUCTORS
        public GeometryRotation(string layername, OSCMessenger messenger, ActionManager actionmanager)
        : this
        (
            actionmanager: actionmanager,
            messageaddress: layername + "/",
            messenger: messenger,
            rotationMode: default,
            rotationX: true,
            rotationY: true,
            rotationZ: true
        )
        { }

        public GeometryRotation
            (
                ActionManager actionmanager,
                string messageaddress,
                OSCMessenger messenger,
                bool rotationX,
                bool rotationY,
                bool rotationZ,
                GeometryRotationMode rotationMode
            )
            : base(actionmanager, messenger)
        {
            MessageAddress = messageaddress;
            Messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
            RotationX = rotationX;
            RotationY = rotationY;
            RotationZ = rotationZ;
        }
        #endregion

        #region PROPERTIES
        private GeometryRotationMode _RotationMode;
        [OSC]
        public GeometryRotationMode RotationMode
        {
            get => _RotationMode;
            set
            {
                SetAndNotify(ref _RotationMode, value);
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
                Messenger.SendMessage(MessageAddress + nameof(RotationZ), RotationZ);
            }
        }
        #endregion

        #region COPY/PASTE
        public void Copy(GeometryRotationDTO geometryrotationdto)
        {
            geometryrotationdto.RotationModeDTO = RotationMode;
            geometryrotationdto.RotationX = RotationX;
            geometryrotationdto.RotationY = RotationY;
            geometryrotationdto.RotationZ = RotationZ;
        }

        public void Paste(GeometryRotationDTO geometryrotationdto)
        {
            Messenger.SendEnabled = false;
            RotationMode = geometryrotationdto.RotationModeDTO;
            RotationX = geometryrotationdto.RotationX;
            RotationY = geometryrotationdto.RotationY;
            RotationZ = geometryrotationdto.RotationZ;
            Messenger.SendEnabled = true;
        }
        #endregion
    }
}