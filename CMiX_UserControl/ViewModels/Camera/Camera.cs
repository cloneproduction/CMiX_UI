using System;
using CMiX.Services;
using CMiX.Models;
using System.Collections.ObjectModel;
using Memento;

namespace CMiX.ViewModels
{
    public class Camera : ViewModel
    {
        #region CONSTRUCTORS
        public Camera(ObservableCollection<OSCMessenger> messengers, MasterBeat masterBeat, Mementor mementor)
            : this
            (
                mementor: mementor,
                messengers: messengers,
                messageaddress: "/Camera/",
                rotation: ((CameraRotation)0).ToString(),
                lookAt: ((CameraLookAt)0).ToString(),
                view: ((CameraView)0).ToString(),
                beatModifier: new BeatModifier("/Camera", messengers, masterBeat, mementor),
                fov: new Slider(String.Format("/{0}/{1}", "Camera", "FOV"), messengers, mementor),
                zoom: new Slider(String.Format("/{0}/{1}", "Camera", "Zoom"), messengers, mementor)
            )
        {
        }

        public Camera
            (
                Mementor mementor,
                string rotation,
                string lookAt,
                string view,
                BeatModifier beatModifier,
                Slider fov,
                Slider zoom,
                ObservableCollection<OSCMessenger> messengers,
                string messageaddress
            )
            : base(messengers, mementor)
        {
            
            Rotation = rotation;
            LookAt = lookAt;
            View = view;
            BeatModifier = beatModifier ?? throw new ArgumentNullException(nameof(beatModifier));
            FOV = fov;
            Zoom = zoom;
            MessageAddress = messageaddress;
            Messengers = messengers ?? throw new ArgumentNullException(nameof(messengers));
            Mementor = mementor;
        }
        #endregion

        #region PROPERTIES
        public BeatModifier BeatModifier { get; }
        public Slider FOV { get; }
        public Slider Zoom { get; }

        private string _rotation;
        [OSC]
        public string Rotation
        {
            get => _rotation;
            set
            {
                if(Mementor != null)
                    Mementor.PropertyChange(this, "Rotation");
                SetAndNotify(ref _rotation, value);
                SendMessages(MessageAddress + nameof(Rotation), Rotation);
            }
        }

        private string _lookAt;
        [OSC]
        public string LookAt
        {
            get => _lookAt;
            set
            {
                if (Mementor != null)
                    Mementor.PropertyChange(this, "LookAt");
                SetAndNotify(ref _lookAt, value);
                SendMessages(MessageAddress + nameof(LookAt), LookAt);
            }
        }

        private string _view;
        [OSC]
        public string View
        {
            get => _view;
            set
            {
                if (Mementor != null)
                    Mementor.PropertyChange(this, "View");
                SetAndNotify(ref _view, value);
                SendMessages(MessageAddress + nameof(View), View);
            }
        }
        #endregion

        #region COPY/PASTE
        public void Copy(CameraDTO cameradto)
        {
            cameradto.Rotation = Rotation;
            cameradto.LookAt = LookAt;
            cameradto.View = View;
            BeatModifier.Copy(cameradto.BeatModifierDTO);
            FOV.Copy(cameradto.FOV);
            Zoom.Copy(cameradto.Zoom);
        }

        public void Paste(CameraDTO cameradto)
        {
            DisabledMessages();

            Rotation = cameradto.Rotation;
            LookAt = cameradto.LookAt;
            View = cameradto.View;
            BeatModifier.Paste(cameradto.BeatModifierDTO);
            FOV.Paste(cameradto.FOV);
            Zoom.Paste(cameradto.Zoom);

            EnabledMessages();
        }
        #endregion
    }
}