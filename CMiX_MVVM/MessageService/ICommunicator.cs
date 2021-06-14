namespace CMiX.MVVM.MessageService
{
    public interface ICommunicator
    {
        IMessageReceiver MessageReceiver { get; set; }
        IMessageSender MessageSender { get; set; }

        void SetCommunicator(ICommunicator communicator);
        void UnsetCommunicator(ICommunicator communicator);
    }
}