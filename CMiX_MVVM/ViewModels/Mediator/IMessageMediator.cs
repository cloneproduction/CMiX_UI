using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels.Mediator
{
    public interface IMessageMediator
    {
        void Notify(MessageDirection messageDirection, Message message);
        void RegisterColleague(IColleague colleague);
    }
}
