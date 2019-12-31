using System;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using System.Collections.ObjectModel;
using Memento;
using CMiX.MVVM.Services;

namespace CMiX.ViewModels
{
    public class Camera : ViewModel, ISendable, IUndoable
    {
        #region CONSTRUCTORS
        public Camera(Messenger messenger, MasterBeat masterBeat, Mementor mementor) 
        {
            MessageAddress = "/Camera/";
            Messenger = messenger;
            Rotation = ((CameraRotation)0).ToString();
            LookAt = ((CameraLookAt)0).ToString();
            View = ((CameraView)0).ToString();

            BeatModifier = new BeatModifier(MessageAddress, masterBeat, messenger, mementor);
            FOV = new Slider(MessageAddress + nameof(FOV), messenger, mementor);
            Zoom = new Slider(MessageAddress + nameof(Zoom), messenger, mementor);
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
                //SendMessages(MessageAddress + nameof(Rotation), Rotation);
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
                //SendMessages(MessageAddress + nameof(LookAt), LookAt);
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
                //SendMessages(MessageAddress + nameof(View), View);
            }
        }

        public string MessageAddress { get; set; }
        public Messenger Messenger { get; set; }
        public Mementor Mementor { get; set; }
        #endregion

        #region COPY/PASTE/RESET
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
            Messenger.Disable();

            MessageAddress = cameramodel.MessageAddress;
            Rotation = cameramodel.Rotation;
            LookAt = cameramodel.LookAt;
            View = cameramodel.View;

            BeatModifier.Paste(cameramodel.BeatModifierModel);
            FOV.Paste(cameramodel.FOV);
            Zoom.Paste(cameramodel.Zoom);

            Messenger.Enable();
        }

        public void Reset()
        {
            Messenger.Disable();

            Rotation = ((CameraRotation)0).ToString();
            LookAt = ((CameraLookAt)0).ToString();
            View = ((CameraView)0).ToString();
            BeatModifier.Reset();
            FOV.Reset();
            Zoom.Reset();

            Messenger.Enable();
        }
        #endregion
    }
}