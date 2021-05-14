using CMiX.MVVM.ViewModels.MessageService;

namespace CMiX.MVVM.ViewModels.Messages
{
    public interface IMessagePack
    {
        IMessageIterator CreateIterator();
        void AddMessage(IMessage message);
    }
}