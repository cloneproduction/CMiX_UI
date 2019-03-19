using System;
using CMiX.Services;
using CMiX.Models;
using System.Collections.ObjectModel;
using Memento;

namespace CMiX.ViewModels
{
    public class GeometryRotation :ViewModel
    {
        #region CONSTRUCTORS
        public GeometryRotation(string layername, ObservableCollection<OSCMessenger> messengers, Mementor mementor)
        : this
        (
            mementor: mementor,
            messageaddress: layername + "/",
            messengers: messengers,
            rotationMode: default,
            rotationX: true,
            rotationY: true,
            rotationZ: true
        )
        { }

        public GeometryRotation
            (
                Mementor mementor,
                string messageaddress,
                ObservableCollection<OSCMessenger> messengers,
                bool rotationX,
                bool rotationY,
                bool rotationZ,
                GeometryRotationMode rotationMode
            )
            : base(messengers)
        {
            MessageAddress = messageaddress;
            Messengers = messengers ?? throw new ArgumentNullException(nameof(messengers));
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
                Mementor.PropertyChange(this, "RotationMode");
                SetAndNotify(ref _RotationMode, value);
                SendMessages(MessageAddress + nameof(RotationMode), RotationMode);
            }
        }

        private bool _RotationX;
        [OSC]
        public bool RotationX
        {
            get => _RotationX;
            set
            {
                Mementor.PropertyChange(this, "RotationX");
                SetAndNotify(ref _RotationX, value);
                SendMessages(MessageAddress + nameof(RotationX), RotationX);
            }
        }

        private bool _RotationY;
        [OSC]
        public bool RotationY
        {
            get => _RotationY;
            set
            {
                Mementor.PropertyChange(this, "RotationY");
                SetAndNotify(ref _RotationY, value);
                SendMessages(MessageAddress + nameof(RotationY), RotationY);
            }
        }

        private bool _RotationZ;
        [OSC]
        public bool RotationZ
        {
            get => _RotationZ;
            set
            {
                Mementor.PropertyChange(this, "RotationZ");
                SetAndNotify(ref _RotationZ, value);
                SendMessages(MessageAddress + nameof(RotationZ), RotationZ);
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
            DisabledMessages();
            RotationMode = geometryrotationdto.RotationModeDTO;
            RotationX = geometryrotationdto.RotationX;
            RotationY = geometryrotationdto.RotationY;
            RotationZ = geometryrotationdto.RotationZ;
            EnabledMessages();
        }
        #endregion
    }
}