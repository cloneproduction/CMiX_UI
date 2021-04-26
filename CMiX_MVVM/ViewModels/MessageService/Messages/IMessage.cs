using System;
using System.Collections.Generic;

namespace CMiX.MVVM.ViewModels.MessageService
{
    public interface IMessage
    {
        Guid ComponentID { get; set; }
    }
}