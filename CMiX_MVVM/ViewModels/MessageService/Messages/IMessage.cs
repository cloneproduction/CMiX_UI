using System;

namespace CMiX.MVVM.ViewModels.MessageService
{
    public interface IMessage
    {
        Guid ComponentID { get; set; }
    }
}