﻿using System;
using CMiX.Services;
//using CMiX.Models;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using System.Collections.ObjectModel;
using Memento;

namespace CMiX.ViewModels
{
    public class GeometryRotation :ViewModel
    {
        #region CONSTRUCTORS
        public GeometryRotation(string messageaddress, ObservableCollection<OSCValidation> oscvalidation, Mementor mementor) : base (oscvalidation, mementor)
        {
            MessageAddress = String.Format("{0}/", messageaddress);
            Mode = default;
            RotationX = true;
            RotationY = true;
            RotationZ = true;
        }
        #endregion

        #region METHODS
        public void UpdateMessageAddress(string messageaddress)
        {
            MessageAddress = messageaddress;
        }
        #endregion

        #region PROPERTIES
        private GeometryRotationMode _Mode;
        public GeometryRotationMode Mode
        {
            get => _Mode;
            set
            {
                if (Mementor != null)
                    Mementor.PropertyChange(this, nameof(Mode));
                SetAndNotify(ref _Mode, value);
                SendMessages(MessageAddress + nameof(Mode), Mode);
            }
        }

        private bool _RotationX;
        public bool RotationX
        {
            get => _RotationX;
            set
            {
                if (Mementor != null)
                    Mementor.PropertyChange(this, nameof(RotationX));
                SetAndNotify(ref _RotationX, value);
                SendMessages(MessageAddress + nameof(RotationX), RotationX);
            }
        }

        private bool _RotationY;
        public bool RotationY
        {
            get => _RotationY;
            set
            {
                if (Mementor != null)
                    Mementor.PropertyChange(this, nameof(RotationY));
                SetAndNotify(ref _RotationY, value);
                SendMessages(MessageAddress + nameof(RotationY), RotationY);
            }
        }

        private bool _RotationZ;
        public bool RotationZ
        {
            get => _RotationZ;
            set
            {
                if (Mementor != null)
                    Mementor.PropertyChange(this, nameof(RotationZ));
                SetAndNotify(ref _RotationZ, value);
                SendMessages(MessageAddress + nameof(RotationZ), RotationZ);
            }
        }
        #endregion

        #region COPY/PASTE/RESET
        public void Reset()
        {
            DisabledMessages();
            Mode = default;
            EnabledMessages();
        }

        public void Copy(GeometryRotationModel geometryrotationmodel)
        {
            geometryrotationmodel.MessageAddress = MessageAddress;
            geometryrotationmodel.Mode = Mode;
            geometryrotationmodel.RotationX = RotationX;
            geometryrotationmodel.RotationY = RotationY;
            geometryrotationmodel.RotationZ = RotationZ;
        }

        public void Paste(GeometryRotationModel geometryrotationmodel)
        {
            DisabledMessages();
            MessageAddress = geometryrotationmodel.MessageAddress;
            Mode = geometryrotationmodel.Mode;
            RotationX = geometryrotationmodel.RotationX;
            RotationY = geometryrotationmodel.RotationY;
            RotationZ = geometryrotationmodel.RotationZ;
            EnabledMessages();
        }
        #endregion
    }
}