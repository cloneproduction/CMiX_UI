using CMiX.MVVM.ViewModels.MessageService;
using System;

namespace CMiX.MVVM.ViewModels.MessageService.Messages
{
    public interface IViewModelMessage : IMessage
    {
        Guid ModuleID { get; set; }

        void Process(Module messageCommunicator);
    }
}
