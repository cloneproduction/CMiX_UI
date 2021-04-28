using CMiX.MVVM.ViewModels.Messages;

namespace CMiX.MVVM.ViewModels.MessageService.ModuleMessenger
{
    public interface IMessageProcessor
    {
        void ProcessMessage(IMessageIterator messageIterator);
    }
}
