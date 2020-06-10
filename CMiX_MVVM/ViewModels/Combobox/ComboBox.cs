using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public class ComboBox<T> : Sendable
    {
        public ComboBox()
        {

        }

        private T _selection;
        public T Selection
        {
            get => _selection;
            set
            {
                SetAndNotify(ref _selection, value);
                OnSendChange(this.GetModel(), this.GetMessageAddress());
            }
        }

        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {
            if (e.ParentMessageAddress + this.GetMessageAddress() == e.MessageAddress)
                this.SetViewModel(e.Model as ComboBoxModel<T>);
            else
                OnReceiveChange(e.Model, e.MessageAddress, e.ParentMessageAddress + this.GetMessageAddress());
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
