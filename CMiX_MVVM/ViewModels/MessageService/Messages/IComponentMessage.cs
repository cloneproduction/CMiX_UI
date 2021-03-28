using CMiX.MVVM.ViewModels.MessageService;

namespace CMiX.MVVM.ViewModels.MessageService
{
    public interface IComponentMessage : IMessage
    {
        void Process(IMessageProcessor viewModel, IMessageTerminal messageTerminal);
    }
}
