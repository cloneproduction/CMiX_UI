using CMiX.MVVM.Interfaces;
using CMiX.MVVM.MessageService;
using CMiX.MVVM.Models;

namespace CMiX.MVVM.ViewModels
{
    public class PostFX : Control
    {
        public PostFX(PostFXModel postFXModel)
        {
            ID = postFXModel.ID;
            Feedback = new Slider(nameof(Feedback), postFXModel.Feedback);
            Blur = new Slider(nameof(Blur), postFXModel.Blur);

            Transforms = postFXModel.Transforms;
            View = postFXModel.View;
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

        public override void SetCommunicator(Communicator communicator)
        {
            base.SetCommunicator(communicator);

            Feedback.SetCommunicator(Communicator);
            Blur.SetCommunicator(Communicator);
        }

        public override void SetViewModel(IModel model)
        {
            PostFXModel postFXModel = model as PostFXModel;
            this.ID = postFXModel.ID;
            this.Transforms = postFXModel.Transforms;
            this.View = postFXModel.View;
            this.Feedback.SetViewModel(postFXModel.Feedback);
            this.Blur.SetViewModel(postFXModel.Blur);
        }

        public override IModel GetModel()
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