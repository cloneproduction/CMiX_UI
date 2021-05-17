using CMiX.MVVM.Interfaces;

namespace CMiX.MVVM.MessageService
{
    public interface IViewModelMessage
    {
        IModel Model { get; set; }
    }
}
