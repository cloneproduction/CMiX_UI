﻿using CMiX.MVVM.Interfaces;
using CMiX.MVVM.MessageService;
using CMiX.MVVM.Models;
using System;

namespace CMiX.MVVM.ViewModels
{
    public class PostFX : ViewModel, IControl
    {
        public PostFX(PostFXModel postFXModel)
        {
            ID = postFXModel.ID;
            Feedback = new Slider(nameof(Feedback), postFXModel.Feedback);
            Blur = new Slider(nameof(Blur), postFXModel.Blur);

            Transforms = postFXModel.Transforms;
            View = postFXModel.View;
        }


        public Guid ID { get; set; }
        public ControlCommunicator Communicator { get; set; }
        public Slider Feedback { get; set; }
        public Slider Blur { get; set; }

        private string _transforms;
        public string Transforms
        {
            get => _transforms;
            set
            {
                SetAndNotify(ref _transforms, value);

            }
        }

        private string _view;
        public string View
        {
            get => _view;
            set
            {
                SetAndNotify(ref _view, value);

            }
        }


        public void SetCommunicator(Communicator communicator)
        {
            Communicator = new ControlCommunicator(this);
            Communicator.SetCommunicator(communicator);

            Feedback.SetCommunicator(Communicator);
            Blur.SetCommunicator(Communicator);
        }

        public void UnsetCommunicator(Communicator communicator)
        {
            Communicator.UnsetCommunicator(communicator);

            Feedback.UnsetCommunicator(Communicator);
            Blur.UnsetCommunicator(Communicator);
        }


        public void SetViewModel(IModel model)
        {
            PostFXModel postFXModel = model as PostFXModel;
            this.ID = postFXModel.ID;
            this.Transforms = postFXModel.Transforms;
            this.View = postFXModel.View;
            this.Feedback.SetViewModel(postFXModel.Feedback);
            this.Blur.SetViewModel(postFXModel.Blur);
        }

        public IModel GetModel()
        {
            PostFXModel postFXModel = new PostFXModel();
            postFXModel.ID = this.ID;
            postFXModel.Feedback = (SliderModel)this.Feedback.GetModel();
            postFXModel.Blur = (SliderModel)this.Blur.GetModel();
            postFXModel.Transforms = this.Transforms;
            postFXModel.View = this.View;
            return postFXModel;
        }
    }
}