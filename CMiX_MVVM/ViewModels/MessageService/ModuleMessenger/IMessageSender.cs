namespace CMiX.MVVM.ViewModels.MessageService
{
    public interface IMessageSender
    {
        void SendMessage(IMessage message);
    }
}