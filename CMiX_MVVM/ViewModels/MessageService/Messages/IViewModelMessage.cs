using CMiX.MVVM.Interfaces;
using System;

namespace CMiX.MVVM.ViewModels.MessageService.Messages
{
    public interface IViewModelMessage : IMessage
    {
        Guid ModuleID { get; set; }
        IModel Model { get; set; }
    }
}
