﻿using CMiX.MVVM.ViewModels.MessageService.MessageSendCOR;
using System;

namespace CMiX.MVVM.ViewModels.MessageService.ModuleMessenger
{
    public class ComponentMessageSender : IMessageDispatcher
    {
        public ComponentMessageSender()
        {

        }


        public IMessage SetMessageID(IMessage message)
        {
            return message;
        }


        private IMessageDispatcher _nextHandler;
        public IMessageDispatcher SetNextSender(IMessageDispatcher handler)
        {
            _nextHandler = handler;
            return handler;
        }


        public void ProcessMessage(IMessage message)
        {
            if (_nextHandler != null)
            {
                _nextHandler.ProcessMessage(message);
                Console.WriteLine("ComponentMessageSender SendMessage");
            }
        }
    }
}