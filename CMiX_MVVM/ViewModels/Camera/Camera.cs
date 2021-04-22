﻿using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.Beat;
using CMiX.MVVM.ViewModels.MessageService;
using CMiX.MVVM.ViewModels.MessageService.ModuleMessenger;

namespace CMiX.MVVM.ViewModels
{
    public class Camera : Module
    {
        public Camera(MasterBeat beat, CameraModel cameraModel) 
        {
            Rotation = ((CameraRotation)0).ToString();
            LookAt = ((CameraLookAt)0).ToString();
            View = ((CameraView)0).ToString();

            BeatModifier = new BeatModifier(beat, new BeatModifierModel());
            //FOV = new Slider(nameof(FOV), messageDispatcher, cameraModel.FOV);
            //Zoom = new Slider(nameof(Zoom), messageDispatcher, cameraModel.Zoom);
        }

        public override void SetReceiver(IMessageReceiver<Module> messageReceiver)
        {
            messageReceiver?.RegisterReceiver(this, ID);
            BeatModifier.SetReceiver(messageReceiver);
        }



        public BeatModifier BeatModifier { get; set; }
        public Slider FOV { get; set; }
        public Slider Zoom { get; set; }

        private string _rotation;
        public string Rotation
        {
            get => _rotation;
            set => SetAndNotify(ref _rotation, value);
        }

        private string _lookAt;
        public string LookAt
        {
            get => _lookAt;
            set => SetAndNotify(ref _lookAt, value);
        }

        private string _view;
        public string View
        {
            get => _view;
            set => SetAndNotify(ref _view, value);
        }

        public override void SetViewModel(IModel model)
        {
            CameraModel cameraModel = model as CameraModel;
            this.Rotation = cameraModel.Rotation;
            this.LookAt = cameraModel.LookAt;
            this.View = cameraModel.View;

            this.BeatModifier.SetViewModel(cameraModel.BeatModifierModel);
            this.FOV.SetViewModel(cameraModel.FOV);
            this.Zoom.SetViewModel(cameraModel.Zoom);
        }

        public override IModel GetModel()
        {
            CameraModel cameraModel = new CameraModel();

            cameraModel.Rotation = this.Rotation;
            cameraModel.LookAt = this.LookAt;
            cameraModel.View = this.View;

            return cameraModel;
        }
    }
}