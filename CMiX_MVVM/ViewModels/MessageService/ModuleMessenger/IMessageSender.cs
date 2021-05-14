using CMiX.MVVM.ViewModels.Messages;

namespace CMiX.MVVM.ViewModels.MessageService
{
    public interface IMessageSender
    {
        void SendMessagePack(IMessagePack messagePack);
    }
}