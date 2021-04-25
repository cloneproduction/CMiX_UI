﻿using System;
using System.Collections.Generic;

namespace CMiX.MVVM.ViewModels.MessageService
{
    public interface IMessage
    {
        List<Guid> IDs { get; set; }
        Guid ComponentID { get; set; }
    }
}