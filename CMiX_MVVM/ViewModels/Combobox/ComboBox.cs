using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.MessageService;

namespace CMiX.MVVM.ViewModels
{
    public class ComboBox<T> : MessageCommunicator
    {
        public ComboBox(MessageDispatcher messageDispatcher) : base(messageDispatcher)
        {

        }

        private T _selection;
        public T Selection
        {
            get => _selection;
            set
            {
                SetAndNotify(ref _selection, value);
                RaiseMessageNotification();
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