using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels;

namespace CMiX.MVVM.MessageService
{
    public class MessageUpdateViewModel : Message, IViewModelMessage
    {
        public MessageUpdateViewModel()
        {

        }

        public MessageUpdateViewModel(IModel model)
        {
            Model = model;
        }

        public MessageUpdateViewModel(Control control)
        {
            Model = control.GetModel();
        }

        public IModel Model { get; set; }
    }
}