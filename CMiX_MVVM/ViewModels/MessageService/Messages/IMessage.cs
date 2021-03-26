using CMiX.MVVM.ViewModels.MessageService;

namespace CMiX.MVVM.ViewModels.MessageService
{
    public interface IMessage
    {
        string Address { get; set; }
        void Process(IMessageProcessor viewModel);
    }
}
