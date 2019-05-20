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
        public GeometryRotation(string layername, ObservableCollection<OSCMessenger> oscmessengers, Mementor mementor) : base (oscmessengers, mementor)
        {
            MessageAddress = layername + "/";
            RotationMode = default;
            RotationX = true;
            RotationY = true;
            RotationZ = true;
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
                if (Mementor != null)
                    Mementor.PropertyChange(this, "RotationMode");
                SetAndNotify(ref _RotationMode, value);
                SendMessages(MessageAddress +"RotationMode", RotationMode);
            }
        }

        private bool _RotationX;
        [OSC]
        public bool RotationX
        {
            get => _RotationX;
            set
            {
                if (Mementor != null)
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
                if (Mementor != null)
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
                if (Mementor != null)
                    Mementor.PropertyChange(this, "RotationZ");
                SetAndNotify(ref _RotationZ, value);
                SendMessages(MessageAddress + nameof(RotationZ), RotationZ);
            }
        }
        #endregion

        #region COPY/PASTE
        public void Copy(GeometryRotationModel geometryrotationdto)
        {
            geometryrotationdto.RotationModeDTO = RotationMode;
            geometryrotationdto.RotationX = RotationX;
            geometryrotationdto.RotationY = RotationY;
            geometryrotationdto.RotationZ = RotationZ;
        }

        public void Paste(GeometryRotationModel geometryrotationdto)
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