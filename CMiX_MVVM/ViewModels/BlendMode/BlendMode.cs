using CMiX.MVVM.Interfaces;
using CMiX.MVVM.MessageService;
using CMiX.MVVM.Models;
using System;

namespace CMiX.MVVM.ViewModels
{
    public class BlendMode : ViewModel, IControl
    {
        public BlendMode(BlendModeModel blendModeModel)
        {
            this.ID = blendModeModel.ID;
            Mode = blendModeModel.Mode;
        }

        public ControlCommunicator Communicator { get; set; }
        public Guid ID { get; set; }

        private string _mode;
        public string Mode
        {
            get { return _mode; }
            set
            {
                SetAndNotify(ref _mode, value);
                Communicator?.SendMessageUpdateViewModel(this);
                Console.WriteLine("BlendModel is " + Mode);
            }
        }

        public void SetCommunicator(Communicator communicator)
        {
            Communicator = new ControlCommunicator(this);
            Communicator.SetCommunicator(communicator);
        }

        public void UnsetCommunicator(Communicator communicator)
        {
            throw new NotImplementedException();
        }

        public void SetViewModel(IModel model)
        {
            BlendModeModel blendModeModel = model as BlendModeModel;
            this.Mode = blendModeModel.Mode;
        }

        public IModel GetModel()
        {
            BlendModeModel model = new BlendModeModel();
            model.ID = this.ID;
            model.Mode = this.Mode;
            return model;
        }
    }
}
