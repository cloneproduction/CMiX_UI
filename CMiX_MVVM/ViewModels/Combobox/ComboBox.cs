using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Mediator;

namespace CMiX.MVVM.ViewModels
{
    public class ComboBox<T> : Sender
    {
        public ComboBox(string name, IMessageProcessor parentSender) : base(name, parentSender)
        {

        }

        private T _selection;
        public T Selection
        {
            get => _selection;
            set
            {
                SetAndNotify(ref _selection, value);
                this.Send(new Message(MessageCommand.UPDATE_VIEWMODEL, this.GetAddress(), this.GetModel()));
            }
        }

        public override IModel GetModel()
        {
            ComboBoxModel<T> model = new ComboBoxModel<T>();
            model.Selection = this.Selection;
            return model;
        }

        public override void SetViewModel(IModel model)
        {
            ComboBoxModel<T> comboBoxModel = model as ComboBoxModel<T>;
            this.Selection = comboBoxModel.Selection;
        }
    }
}