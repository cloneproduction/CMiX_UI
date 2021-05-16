namespace CMiX.MVVM.MessageService
{
    public interface IMessageProcessor
    {
        void ProcessMessage(IMessage message);
        void DispatchIterator(IMessageIterator messageIterator);
    }
}