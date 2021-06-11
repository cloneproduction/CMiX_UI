namespace CMiX.MVVM.MessageService
{
    public interface ICommunicator
    {
        IMessageReceiver MessageReceiver { get; set; }
        IMessageSender MessageSender { get; set; }

        void SetNextCommunicator(ICommunicator communicator);
        void SendMessage<T>(T obj);
    }
}
