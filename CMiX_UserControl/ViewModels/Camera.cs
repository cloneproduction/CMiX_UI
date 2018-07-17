using System;
using CMiX.Services;
using CMiX.Models;

namespace CMiX.ViewModels
{
    public class Camera : ViewModel, IMessengerData
    {
        public Camera(IMessenger messenger, MasterBeat masterBeat)
            : this(
                  messenger: messenger,
                  messageaddress: "/Camera",
                  messageEnabled: true,
                  rotation: ((CameraRotation)0).ToString(),
                  lookAt: ((CameraLookAt)0).ToString(),
                  view: ((CameraView)0).ToString(),
                  beatModifier: new BeatModifier("/Camera", messenger, masterBeat),
                  fov: 0.2,
                  zoom: 1.0
                  )
        {
        }

        public Camera(
            string rotation, 
            string lookAt, 
            string view, 
            BeatModifier beatModifier, 
            double fov, 
            double zoom,
            IMessenger messenger,
            string messageaddress,
            bool messageEnabled
            )
        {
            Rotation = rotation;
            LookAt = lookAt;
            View = view;
            BeatModifier = beatModifier ?? throw new ArgumentNullException(nameof(beatModifier));
            FOV = fov;
            Zoom = zoom;
            MessageEnabled = messageEnabled;
            MessageAddress = messageaddress;
            Messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
        }

        public IMessenger Messenger { get; }

        public string MessageAddress { get; set; }

        public bool MessageEnabled { get; set; }

        public BeatModifier BeatModifier { get; }

        private string _rotation;
        [OSC]
        public string Rotation
        {
            get => _rotation;
            set
            {
                SetAndNotify(ref _rotation, value);
                if(MessageEnabled)
                    Messenger.SendMessage(MessageAddress +"/" + nameof(Rotation), Rotation);
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
                if(MessageEnabled)
                    Messenger.SendMessage(MessageAddress + "/" + nameof(LookAt), LookAt);
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
                if(MessageEnabled)
                    Messenger.SendMessage(MessageAddress + "/" + nameof(View), View);
            }
        }

        private double _FOV;
        [OSC]
        public double FOV
        {
            get => _FOV;
            set
            {
                SetAndNotify(ref _FOV, value);
                if(MessageEnabled)
                    Messenger.SendMessage(MessageAddress + "/" + nameof(FOV), FOV);
            }
        }

        private double _zoom;
        [OSC]
        public double Zoom
        {
            get => _zoom;
            set
            {
                SetAndNotify(ref _zoom, value);
                if(MessageEnabled)
                    Messenger.SendMessage(MessageAddress + "/" + nameof(Zoom), Zoom);
            }
        }

        public void Copy(CameraDTO cameradto)
        {
            cameradto.Rotation = Rotation;
            cameradto.LookAt = LookAt;
            cameradto.View = View;

            BeatModifier.Copy(cameradto.BeatModifierDTO);

            cameradto.FOV = FOV;
            cameradto.Zoom = Zoom;
        }

        public void Paste(CameraDTO cameradto)
        {
            MessageEnabled = false;

            Rotation = cameradto.Rotation;
            LookAt = cameradto.LookAt;
            View = cameradto.View;

            BeatModifier.Paste(cameradto.BeatModifierDTO);

            FOV = cameradto.FOV;
            Zoom = cameradto.Zoom;

            MessageEnabled = true;
        }
    }
}