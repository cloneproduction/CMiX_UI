﻿using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.Mediator;
using CMiX.MVVM.ViewModels.MessageService.Messages;

namespace CMiX.MVVM.ViewModels
{
    public class PostFX : MessageCommunicator
    {
        public PostFX(string name, IMessageProcessor parentSender) : base (name, parentSender) 
        {
            Feedback = new Slider(nameof(Feedback), this);
            Blur = new Slider(nameof(Blur), this);

            Transforms = ((PostFXTransforms)0).ToString();
            View = ((PostFXView)0).ToString();
        }

        public Slider Feedback { get; set; }
        public Slider Blur { get; set; }

        private string _transforms;
        public string Transforms
        {
            get => _transforms;
            set
            {
                SetAndNotify(ref _transforms, value);
                this.MessageDispatcher.NotifyOut(new MessageUpdateViewModel(this.GetAddress(), this.GetModel()));
            }
        }

        private string _view;
        public string View
        {
            get => _view;
            set
            {
                SetAndNotify(ref _view, value);
                this.MessageDispatcher.NotifyOut(new MessageUpdateViewModel(this.GetAddress(), this.GetModel()));
            }
        }

        public override void SetViewModel(IModel model)
        {
            PostFXModel postFXModel = model as PostFXModel;
            this.Transforms = postFXModel.Transforms;
            this.View = postFXModel.View;
            this.Feedback.SetViewModel(postFXModel.Feedback);
            this.Blur.SetViewModel(postFXModel.Blur);
        }

        public override IModel GetModel()
        {
            PostFXModel postFXModel = new PostFXModel();
            postFXModel.Feedback = (SliderModel)this.Feedback.GetModel();
            postFXModel.Blur = (SliderModel)this.Blur.GetModel();
            postFXModel.Transforms = this.Transforms;
            postFXModel.View = this.View;
            return postFXModel;
        }
    }
}