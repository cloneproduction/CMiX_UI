using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels.Mediator
{
    public interface IMessageMediator
    {
        void Notify(string address, IColleague colleague, Message message);
        void RegisterColleague(string address, IColleague colleague);
    }
}
