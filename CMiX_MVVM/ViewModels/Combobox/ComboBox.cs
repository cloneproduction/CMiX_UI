using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Mediator;

namespace CMiX.MVVM.ViewModels
{
    public class ComboBox<T> : Sender
    {
        public ComboBox(string name, IColleague parentSender) : base(name, parentSender)
        {

        }

        public override void Receive(Message message)
        {
            this.SetViewModel(message.Obj as ComboBoxModel<T>);
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

        public ComboBoxModel<T> GetModel()
        {
            ComboBoxModel<T> model = new ComboBoxModel<T>();
            model.Selection = this.Selection;
            return model;
        }

        public void SetViewModel(ComboBoxModel<T> model)
        {
            this.Selection = model.Selection;
        }
    }
}