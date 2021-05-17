namespace CMiX.MVVM.MessageService
{
    public interface IMessageProcessor
    {
        void ProcessMessage(Message message);
        void DispatchIterator(IIDIterator messageIterator);
    }
}