using CMiX.Core.Interfaces;
using CMiX.Core.MessageService;
using CMiX.Core.Models;
using System;

namespace CMiX.Core.Presentation.ViewModels
{
    public class ComboBox<T> : ViewModel, IControl
    {
        public ComboBox(ComboBoxModel<T> comboBoxModel)
        {
            this.ID = comboBoxModel.ID;
        }


        public Guid ID { get; set; }
        public ControlCommunicator Communicator { get; set; }


        private T _selection;
        public T Selection
        {
            get => _selection;
            set
            {
                SetAndNotify(ref _selection, value);
                Communicator?.SendMessageUpdateViewModel(this);
                Console.WriteLine("Combobox Selection is " + Selection.ToString());
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
            ComboBoxModel<T> model = new ComboBoxModel<T>();
            model.ID = this.ID;
            model.Selection = this.Selection;
            return model;
        }

        public void SetViewModel(IModel model)
        {
            ComboBoxModel<T> comboBoxModel = model as ComboBoxModel<T>;
            this.ID = comboBoxModel.ID;
            this.Selection = comboBoxModel.Selection;
        }
    }
}