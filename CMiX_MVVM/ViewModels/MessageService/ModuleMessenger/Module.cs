﻿using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels.MessageService;
using CMiX.MVVM.ViewModels.MessageService.Messages;
using CMiX.MVVM.ViewModels.MessageService.ModuleMessenger;
using System;

namespace CMiX.MVVM.ViewModels
{
    public abstract class Module : ViewModel, IMessageCommunicator
    {
        public Guid ID { get; set; }


        public ModuleMessageSender MessageSender;


        public void SetSender(IMessageSender messageSender)
        {
            MessageSender = messageSender as ModuleMessageSender;
        }


        public virtual void SetReceiver(IMessageReceiver messageReceiver)
        {
            messageReceiver.RegisterReceiver(this);
        }

        public abstract void SetViewModel(IModel model);
        public abstract IModel GetModel();
    }
}