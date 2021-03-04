using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Mediator;
using CMiX.MVVM.ViewModels.MessageService.Messages;

namespace CMiX.MVVM.ViewModels
{
    public class BlendMode : MessageCommunicator
    {
        public BlendMode(IMessageProcessor parentSender)  : base (parentSender)
        {
            Mode = ((BlendModeEnum)0).ToString();
        }

        private string _mode;
        public string Mode
        {
            get { return _mode; }
            set
            {
                SetAndNotify(ref _mode, value);
                this.MessageDispatcher.NotifyOut(new MessageUpdateViewModel(this.GetAddress(), this.GetModel()));
            }
        }

        public override void SetViewModel(IModel model)
        {
            BlendModeModel blendModeModel = model as BlendModeModel;
            this.Mode = blendModeModel.Mode;
        }

        public override IModel GetModel()
        {
            BlendModeModel model = new BlendModeModel();
            model.Mode = this.Mode;
            return model;
        }
    }
}
