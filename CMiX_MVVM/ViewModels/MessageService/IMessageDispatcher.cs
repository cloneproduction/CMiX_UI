namespace CMiX.MVVM.ViewModels.MessageService
{
    public interface IMessageDispatcher
    {
        void ProcessMessage(IMessage message);
    }
}