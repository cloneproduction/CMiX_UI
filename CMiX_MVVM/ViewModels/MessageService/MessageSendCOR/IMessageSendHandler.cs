namespace CMiX.MVVM.ViewModels.MessageService.MessageSendCOR
{
    public interface IMessageSendHandler
    {
        IMessageSendHandler SetSender(IMessageSendHandler handler);
        void SendMessage(IMessage message);
    }
}