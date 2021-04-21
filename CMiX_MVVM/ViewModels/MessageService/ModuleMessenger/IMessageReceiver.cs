namespace CMiX.MVVM.ViewModels.MessageService
{
    public interface IMessageReceiver
    {
        void RegisterReceiver(IMessageCommunicator messageCommunicator);

        void UnregisterReceiver(IMessageCommunicator component);

        void ReceiveMessage(IMessage message);
    }
}