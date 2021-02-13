using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Mediator;
using CMiX.MVVM.ViewModels.MessageService.Messages;

namespace CMiX.MVVM.ViewModels
{
    public class PostFX : Sender
    {
        public PostFX(string name, IMessageProcessor parentSender) : base (name, parentSender) 
        {
            Feedback = new Slider(nameof(Feedback), this);
            Blur = new Slider(nameof(Blur), this);

            Transforms = ((PostFXTransforms)0).ToString();
            View = ((PostFXView)0).ToString();
        }

        public override void Receive(IMessage message)
        {
            var mess = message.Obj as PostFXModel;
            this.SetViewModel(mess);
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
                this.Send(new Message(MessageCommand.UPDATE_VIEWMODEL, this.GetAddress(), this.GetModel()));
            }
        }

        private string _view;
        public string View
        {
            get => _view;
            set
            {
                SetAndNotify(ref _view, value);
                this.Send(new Message(MessageCommand.UPDATE_VIEWMODEL, this.GetAddress(), this.GetModel()));
            }
        }
    }
}