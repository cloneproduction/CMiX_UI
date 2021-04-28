using CMiX.MVVM.ViewModels.MessageService;

namespace CMiX.MVVM.ViewModels.Messages
{
    public interface IMessageIterator
    {
        IMessage Next();
    }
}