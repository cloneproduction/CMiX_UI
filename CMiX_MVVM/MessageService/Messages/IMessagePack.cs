
namespace CMiX.MVVM.MessageService
{
    public interface IMessagePack
    {
        IMessageIterator CreateIterator();
        void AddMessage(IMessage message);
    }
}