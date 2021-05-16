using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;

namespace CMiX.MVVM.ViewModels
{
    public class BlendMode : Control
    {
        public BlendMode(BlendModeModel blendModeModel)
        {
            Mode = blendModeModel.Mode;
        }


        //public override void SetReceiver(IMessageReceiver messageReceiver)
        //{
        //    //messageReceiver?.RegisterReceiver(this, ID);
        //}


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
