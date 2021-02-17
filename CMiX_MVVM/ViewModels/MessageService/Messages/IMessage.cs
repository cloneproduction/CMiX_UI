using CMiX.MVVM.ViewModels.Mediator;

namespace CMiX.MVVM.ViewModels.MessageService
{
    public interface IMessage
    {
        string Address { get; set; }
        void Process(IMessageProcessor viewModel);
    }
}
