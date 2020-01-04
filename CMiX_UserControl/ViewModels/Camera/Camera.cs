using System;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using System.Collections.ObjectModel;
using Memento;
using CMiX.MVVM.Services;
using CMiX.MVVM;

namespace CMiX.ViewModels
{
    public class Camera : ViewModel, ICopyPasteModel, ISendable, IUndoable
    {
        #region CONSTRUCTORS
        public Camera(Messenger messenger, string messageAddress, MasterBeat masterBeat, Mementor mementor) 
        {
            Messenger = messenger;
            MessageAddress = $"{messageAddress}{nameof(Camera)}/";
            
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
        public void CopyModel(IModel model)
        {
            CameraModel cameraModel = model as CameraModel;
            cameraModel.MessageAddress = MessageAddress;
            cameraModel.Rotation = Rotation;
            cameraModel.LookAt = LookAt;
            cameraModel.View = View;
            BeatModifier.CopyModel(cameraModel.BeatModifierModel);
            FOV.CopyModel(cameraModel.FOV);
            Zoom.CopyModel(cameraModel.Zoom);
        }

        public void PasteModel(IModel model)
        {
            CameraModel cameraModel = model as CameraModel;
            Messenger.Disable();

            MessageAddress = cameraModel.MessageAddress;
            Rotation = cameraModel.Rotation;
            LookAt = cameraModel.LookAt;
            View = cameraModel.View;

            BeatModifier.PasteModel(cameraModel.BeatModifierModel);
            FOV.PasteModel(cameraModel.FOV);
            Zoom.PasteModel(cameraModel.Zoom);

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