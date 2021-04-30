using CMiX.MVVM.Interfaces;
using System;

namespace CMiX.MVVM.ViewModels.MessageService.Messages
{
    public interface IViewModelMessage : IMessage
    {
        IModel Model { get; set; }
    }
}
