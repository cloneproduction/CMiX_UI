﻿using System;
using CMiX.Services;
using CMiX.Models;
using System.Collections.ObjectModel;
using Memento;

namespace CMiX.ViewModels
{
    public class Camera : ViewModel
    {
        #region CONSTRUCTORS
        public Camera(ObservableCollection<OSCMessenger> oscmessengers, ObservableCollection<OSCValidation> cansendmessage, MasterBeat masterBeat, Mementor mementor) 
            : base (oscmessengers, cansendmessage, mementor)
        {
            MessageAddress = "/Camera/";

            DisabledMessages();
            Rotation = ((CameraRotation)0).ToString();
            LookAt = ((CameraLookAt)0).ToString();
            View = ((CameraView)0).ToString();

            BeatModifier = new BeatModifier(MessageAddress, oscmessengers, masterBeat, cansendmessage, mementor);
            FOV = new Slider(MessageAddress + nameof(FOV), oscmessengers, cansendmessage, mementor);
            Zoom = new Slider(MessageAddress + nameof(Zoom), oscmessengers, cansendmessage, mementor);
        }

        #endregion

        #region PROPERTIES
        public BeatModifier BeatModifier { get; }
        public Slider FOV { get; }
        public Slider Zoom { get; }

        private string _rotation;
        public string Rotation
        {
            get => _rotation;
            set
            {
                if(Mementor != null)
                    Mementor.PropertyChange(this, nameof(Rotation));
                SetAndNotify(ref _rotation, value);
                SendMessages(MessageAddress + nameof(Rotation), Rotation);
            }
        }

        private string _lookAt;
        public string LookAt
        {
            get => _lookAt;
            set
            {
                if (Mementor != null)
                    Mementor.PropertyChange(this, nameof(LookAt));
                SetAndNotify(ref _lookAt, value);
                SendMessages(MessageAddress + nameof(LookAt), LookAt);
            }
        }

        private string _view;
        public string View
        {
            get => _view;
            set
            {
                if (Mementor != null)
                    Mementor.PropertyChange(this, nameof(View));
                SetAndNotify(ref _view, value);
                SendMessages(MessageAddress + nameof(View), View);
            }
        }
        #endregion

        #region COPY/PASTE/RESET
        public void Reset()
        {
            DisabledMessages();
            Rotation = ((CameraRotation)0).ToString();
            LookAt = ((CameraLookAt)0).ToString();
            View = ((CameraView)0).ToString();
            BeatModifier.Reset();
            FOV.Reset();
            Zoom.Reset();
            EnabledMessages();
        }
        public void Copy(CameraModel cameramodel)
        {
            cameramodel.MessageAddress = MessageAddress;
            cameramodel.Rotation = Rotation;
            cameramodel.LookAt = LookAt;
            cameramodel.View = View;
            BeatModifier.Copy(cameramodel.BeatModifierModel);
            FOV.Copy(cameramodel.FOV);
            Zoom.Copy(cameramodel.Zoom);
        }

        public void Paste(CameraModel cameramodel)
        {
            DisabledMessages();

            MessageAddress = cameramodel.MessageAddress;
            Rotation = cameramodel.Rotation;
            LookAt = cameramodel.LookAt;
            View = cameramodel.View;

            BeatModifier.Paste(cameramodel.BeatModifierModel);
            FOV.Paste(cameramodel.FOV);
            Zoom.Paste(cameramodel.Zoom);

            EnabledMessages();
        }
        #endregion
    }
}