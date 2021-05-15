using CMiX.MVVM.Interfaces;
using System;

namespace CMiX.MVVM.MessageService
{
    public interface IViewModelMessage : IMessage
    {
        IModel Model { get; set; }
    }
}
