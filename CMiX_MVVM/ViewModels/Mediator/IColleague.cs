using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels.Mediator
{
    public interface IColleague
    {
        MessageMediator MessageMediator { get; set; }
        string Address { get; set; }
        void Send(Message message);
        void Receive(Message message);
    }
}
