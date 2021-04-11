using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.MessageService;

namespace CMiX.MVVM.ViewModels
{
    public class BlendMode : MessageCommunicator
    {
        public BlendMode(IMessageDispatcher messageDispatcher, BlendModeModel blendModeModel)
        {
            Mode = blendModeModel.Mode;
        }


        public override void SetModuleReceiver(ModuleMessageReceiver messageDispatcher)
        {
            messageDispatcher.RegisterMessageProcessor(this);
        }


        private string _mode;
        public string Mode
        {
            get { return _mode; }
            set
            {
                SetAndNotify(ref _mode, value);

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
