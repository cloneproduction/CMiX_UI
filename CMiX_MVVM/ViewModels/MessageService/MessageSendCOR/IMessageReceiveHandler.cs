﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.MVVM.ViewModels.MessageService.MessageSendCOR
{
    public interface IMessageReceiveHandler/// : IMessageHandler
    {
        //Guid ID { get; set; }
        void ReceiveMessage(IMessage message);
    }
}