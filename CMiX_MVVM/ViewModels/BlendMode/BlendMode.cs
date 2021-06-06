using CMiX.MVVM.Interfaces;
using CMiX.MVVM.MessageService;
using CMiX.MVVM.Models;

namespace CMiX.MVVM.ViewModels
{
    public class BlendMode : Control
    {
        public BlendMode(BlendModeModel blendModeModel)
        {
            this.ID = blendModeModel.ID;
            Mode = blendModeModel.Mode;
        }

        private string _mode;
        public string Mode
        {
            get { return _mode; }
            set
            {
                SetAndNotify(ref _mode, value);
                MessageSender?.SendMessage(new MessageUpdateViewModel(this.GetModel()));
                System.Console.WriteLine("BlendModel is " + Mode);
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
            model.ID = this.ID;
            model.Mode = this.Mode;
            return model;
        }
    }
}
