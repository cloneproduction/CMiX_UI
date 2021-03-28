namespace CMiX.MVVM.ViewModels.MessageService
{
    public interface IMessageTerminal
    {
        void ProcessMessage(IMessage message);
        void RegisterMessageProcessor(IComponentMessageProcessor messageProcessor);
        void UnregisterMessageProcessor(IComponentMessageProcessor messageProcessor);
    }
}
