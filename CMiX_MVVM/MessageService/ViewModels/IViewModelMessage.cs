using CMiX.MVVM.Interfaces;

namespace CMiX.MVVM.MessageService
{
    public interface IViewModelMessage : IMessage
    {
        IModel Model { get; set; }
    }
}
