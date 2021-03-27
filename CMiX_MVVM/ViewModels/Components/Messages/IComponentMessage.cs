using CMiX.MVVM.ViewModels.MessageService;

namespace CMiX.MVVM.ViewModels.Components.Messages
{
    public interface IComponentMessage : IMessage
    {
        void Process(IMessageProcessor viewModel, IMessageTerminal messageTerminal);
    }
}
