namespace CMiX.MVVM.ViewModels.MessageService
{
    public interface IMessageSender : IMessageDispatcher
    {
        void SendMessage(IMessage message);
    }
}