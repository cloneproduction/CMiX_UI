using CMiX.MVVM.ViewModels.MessageService;
using System;

namespace CMiX.MVVM.ViewModels.MessageService.Messages
{
    public interface IViewModelMessage : IMessage
    {
        void Process(IMessageProcessor viewModel);
        Guid ComponentID { get; set; }
    }
}
