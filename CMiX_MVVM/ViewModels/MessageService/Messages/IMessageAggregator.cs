using CMiX.MVVM.ViewModels.MessageService;

namespace CMiX.MVVM.ViewModels.Messages
{
    public interface IMessageAggregator
    {
        IMessageIterator CreateIterator();
        void AddMessage(IMessage message);
    }
}