using System;
using CMiX.Services;
using CMiX.Models;
using GuiLabs.Undo;

namespace CMiX.ViewModels
{
    public class Camera : ViewModel
    {
        #region CONSTRUCTORS
        public Camera(OSCMessenger messenger, MasterBeat masterBeat, ActionManager actionmanager)
            : this
            (
                actionmanager: actionmanager,
                messenger: messenger,
                messageaddress: "/Camera/",
                rotation: ((CameraRotation)0).ToString(),
                lookAt: ((CameraLookAt)0).ToString(),
                view: ((CameraView)0).ToString(),
                beatModifier: new BeatModifier("/Camera", messenger, masterBeat, actionmanager),
                fov: new Slider(String.Format("/{0}/{1}", "Camera", "FOV"), messenger, actionmanager),
                zoom: new Slider(String.Format("/{0}/{1}", "Camera", "Zoom"), messenger, actionmanager)
            )
        {
        }

        public Camera
            (
                ActionManager actionmanager,
                string rotation,
                string lookAt,
                string view,
                BeatModifier beatModifier,
                Slider fov,
                Slider zoom,
                OSCMessenger messenger,
                string messageaddress
            )
            : base(actionmanager, messenger)
        {
            Rotation = rotation;
            LookAt = lookAt;
            View = view;
            BeatModifier = beatModifier ?? throw new ArgumentNullException(nameof(beatModifier));
            FOV = fov;
            Zoom = zoom;
            MessageAddress = messageaddress;
            Messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
        }
        #endregion

        #region PROPERTIES
        public string MessageAddress { get; set; }

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
                SetAndNotify(ref _rotation, value);
                Messenger.SendMessage(MessageAddress + nameof(Rotation), Rotation);
            }
        }

        private string _lookAt;
        [OSC]
        public string LookAt
        {
            get => _lookAt;
            set
            {
                SetAndNotify(ref _lookAt, value);
                Messenger.SendMessage(MessageAddress + nameof(LookAt), LookAt);
            }
        }

        private string _view;
        [OSC]
        public string View
        {
            get => _view;
            set
            {
                SetAndNotify(ref _view, value);
                Messenger.SendMessage(MessageAddress + nameof(View), View);
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
            Messenger.SendEnabled = false;

            Rotation = cameradto.Rotation;
            LookAt = cameradto.LookAt;
            View = cameradto.View;
            BeatModifier.Paste(cameradto.BeatModifierDTO);
            FOV.Paste(cameradto.FOV);
            Zoom.Paste(cameradto.Zoom);

            Messenger.SendEnabled = true;
        }
        #endregion
    }
}