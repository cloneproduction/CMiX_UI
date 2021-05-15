using System;
using System.Collections.Generic;

namespace CMiX.MVVM.MessageService
{
    public interface IMessage
    {
        Guid ComponentID { get; set; }
    }
}