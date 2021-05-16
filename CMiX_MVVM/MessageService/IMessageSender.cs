namespace CMiX.MVVM.MessageService
{
    public interface IMessageSender
    {
        void SendMessagePack(IMessagePack messagePack);
    }
}