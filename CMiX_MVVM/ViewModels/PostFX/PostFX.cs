using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.MessageService;
using CMiX.MVVM.ViewModels.MessageService.Messages;

namespace CMiX.MVVM.ViewModels
{
    public class PostFX : MessageCommunicator
    {
        public PostFX(MessageDispatcher messageDispatcher, PostFXModel postFXModel) : base (messageDispatcher) 
        {
            Feedback = new Slider(nameof(Feedback), messageDispatcher, postFXModel.Feedback);
            Blur = new Slider(nameof(Blur), messageDispatcher, postFXModel.Blur);

            Transforms = postFXModel.Transforms;// ((PostFXTransforms)0).ToString();
            View = postFXModel.View;// ((PostFXView)0).ToString();
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
                RaiseMessageNotification();
            }
        }

        private string _view;
        public string View
        {
            get => _view;
            set
            {
                SetAndNotify(ref _view, value);
                RaiseMessageNotification();
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