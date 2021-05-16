
namespace CMiX.MVVM.MessageService
{
    public interface IMessageIterator
    {
        IMessage Next();
        bool IsDone { get; }
        IMessage CurrentMessage { get; }
    }
}