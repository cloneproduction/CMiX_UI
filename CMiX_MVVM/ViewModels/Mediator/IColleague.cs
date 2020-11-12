using CMiX.MVVM.Services.Message;

namespace CMiX.MVVM.ViewModels.Mediator
{
    public interface IColleague
    {
        MessageMediator MessageMediator { get; set; }
        string Address { get; set; }
        void Send(MessageReceived message);
        void Receive(MessageReceived message);
    }
}
