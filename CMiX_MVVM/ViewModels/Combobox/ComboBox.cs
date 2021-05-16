using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;

namespace CMiX.MVVM.ViewModels
{
    public class ComboBox<T> : Control
    {
        public ComboBox(ComboBoxModel<T> comboBoxModel) 
        {

        }


        //public override void SetReceiver(IMessageReceiver messageReceiver)
        //{
        //    //messageReceiver?.RegisterReceiver(this, ID);
        //}

        private T _selection;
        public T Selection
        {
            get => _selection;
            set
            {
                SetAndNotify(ref _selection, value);

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