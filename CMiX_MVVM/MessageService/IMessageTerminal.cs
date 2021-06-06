namespace CMiX.MVVM.MessageService
{
    public interface IMessageTerminal
    {
        void SetReceiver(IMessageReceiver messageReceiver);
        void SetSender(IMessageSender messageSender);
    }
}