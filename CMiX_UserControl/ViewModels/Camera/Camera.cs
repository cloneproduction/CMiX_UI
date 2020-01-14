using System;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using System.Collections.ObjectModel;
using Memento;
using CMiX.MVVM.Services;
using CMiX.MVVM;

namespace CMiX.Studio.ViewModels
{
    public class Camera : ViewModel, ISendable, IUndoable
    {
        #region CONSTRUCTORS
        public Camera(MessageService messageService, string messageAddress, MasterBeat masterBeat, Mementor mementor) 
        {
            MessageService = messageService;
            MessageAddress = $"{messageAddress}{nameof(Camera)}/";
            
            Rotation = ((CameraRotation)0).ToString();
            LookAt = ((CameraLookAt)0).ToString();
            View = ((CameraView)0).ToString();

            BeatModifier = new BeatModifier(MessageAddress, masterBeat, messageService, mementor);
            FOV = new Slider(MessageAddress + nameof(FOV), messageService, mementor);
            Zoom = new Slider(MessageAddress + nameof(Zoom), messageService, mementor);
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
        public Mementor Mementor { get; set; }
        public MessageService MessageService { get; set; }
        #endregion

        #region COPY/PASTE/RESET
        public CameraModel GetModel()
        {
            CameraModel cameraModel = new CameraModel();
            cameraModel.Rotation = Rotation;
            cameraModel.LookAt = LookAt;
            cameraModel.View = View;

            return cameraModel;
        }

        //public void CopyModel(CameraModel cameraModel)
        //{
        //    cameraModel.Rotation = Rotation;
        //    cameraModel.LookAt = LookAt;
        //    cameraModel.View = View;
        //    BeatModifier.CopyModel(cameraModel.BeatModifierModel);
        //    FOV.CopyModel(cameraModel.FOV);
        //    Zoom.CopyModel(cameraModel.Zoom);
        //}

        public void PasteModel(CameraModel cameraModel)
        {
            MessageService.Disable();

            Rotation = cameraModel.Rotation;
            LookAt = cameraModel.LookAt;
            View = cameraModel.View;

            BeatModifier.PasteModel(cameraModel.BeatModifierModel);
            FOV.PasteModel(cameraModel.FOV);
            Zoom.PasteModel(cameraModel.Zoom);

            MessageService.Enable();
        }

        public void Reset()
        {
            MessageService.Disable();

            Rotation = ((CameraRotation)0).ToString();
            LookAt = ((CameraLookAt)0).ToString();
            View = ((CameraView)0).ToString();
            BeatModifier.Reset();
            FOV.Reset();
            Zoom.Reset();

            MessageService.Enable();
        }
        #endregion
    }
}