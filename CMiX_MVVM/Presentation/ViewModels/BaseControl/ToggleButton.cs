using CMiX.Core.Models;
using CMiX.Core.Network.Communicators;
using System;

namespace CMiX.Core.Presentation.ViewModels
{
    public class ToggleButton : ViewModel, IControl
    {
        public ToggleButton(ToggleButtonModel toggleButtonModel)
        {
            this.ID = toggleButtonModel.ID;
        }


        public Guid ID { get; set; }
        public ControlCommunicator Communicator { get; set; }

        private bool _isChecked;
        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                SetAndNotify(ref _isChecked, value);
                Communicator?.SendMessageUpdateViewModel(this);
            }
        }


        public void SetCommunicator(Communicator communicator)
        {
            Communicator = new ControlCommunicator(this);
            Communicator.SetCommunicator(communicator);
        }

        public void UnsetCommunicator(Communicator communicator)
        {
            Communicator.UnsetCommunicator(communicator);
        }


        public IModel GetModel()
        {
            ToggleButtonModel model = new ToggleButtonModel();
            model.ID = this.ID;
            model.IsChecked = this.IsChecked;
            return model;
        }

        public void SetViewModel(IModel model)
        {
            ToggleButtonModel comboBoxModel = model as ToggleButtonModel;
            this.ID = comboBoxModel.ID;
            this.IsChecked = comboBoxModel.IsChecked;
        }
    }
}