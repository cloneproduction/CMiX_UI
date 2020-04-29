﻿using System;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using Memento;
using CMiX.MVVM.Services;

namespace CMiX.Studio.ViewModels
{
    public class GeometryRotation : ViewModel, IUndoable
    {
        #region CONSTRUCTORS
        public GeometryRotation(string messageaddress, MessengerService messengerService, Mementor mementor)
        {
            MessageAddress = String.Format("{0}/", messageaddress);
            MessengerService = messengerService;
            Mode = default;
            RotationX = true;
            RotationY = true;
            RotationZ = true;
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
                //SendMessages(MessageAddress + nameof(Mode), Mode);
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
                //SendMessages(MessageAddress + nameof(RotationX), RotationX);
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
                //SendMessages(MessageAddress + nameof(RotationY), RotationY);
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
                //SendMessages(MessageAddress + nameof(RotationZ), RotationZ);
            }
        }

        public string MessageAddress { get; set; }
        public MessengerService MessengerService { get; set; }
        public Mementor Mementor { get; set; }
        #endregion

        #region COPY/PASTE/RESET
        public void Reset()
        {
            MessengerService.Disable();
            Mode = default;
            MessengerService.Enable();
        }

        public void Copy(GeometryRotationModel geometryRotationModel)
        {
            geometryRotationModel.Mode = Mode;
            geometryRotationModel.RotationX = RotationX;
            geometryRotationModel.RotationY = RotationY;
            geometryRotationModel.RotationZ = RotationZ;
        }

        public void Paste(GeometryRotationModel geometryRotationModel)
        {
            MessengerService.Disable();

            Mode = geometryRotationModel.Mode;
            RotationX = geometryRotationModel.RotationX;
            RotationY = geometryRotationModel.RotationY;
            RotationZ = geometryRotationModel.RotationZ;
            MessengerService.Enable();
        }
        #endregion
    }
}