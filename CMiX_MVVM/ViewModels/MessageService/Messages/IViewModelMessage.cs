using CMiX.MVVM.ViewModels.MessageService;

namespace CMiX.MVVM.ViewModels.MessageService.Messages
{
    public interface IViewModelMessage : IMessage
    {
        void Process(IMessageProcessor viewModel);
    }
}
