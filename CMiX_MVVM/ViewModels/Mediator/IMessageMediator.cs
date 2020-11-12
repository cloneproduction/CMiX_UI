using CMiX.MVVM.Services.Message;

namespace CMiX.MVVM.ViewModels.Mediator
{
    public interface IMessageMediator
    {
        void Notify(string address, IColleague colleague, MessageReceived message);
        void RegisterColleague(string address, IColleague colleague);
    }
}
