using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.MessageService.Messages;

namespace CMiX.MVVM.ViewModels.Mediator
{
    public interface IMessageProcessor
    {
        MessageDispatcher MessageDispatcher { get; set; }
        string GetAddress();
        void Send(IMessage message);
        void Receive(IMessage message);
    }
}