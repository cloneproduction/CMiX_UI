using CMiX.MVVM.ViewModels.Components;
using CMiX.MVVM.ViewModels.MessageService.MessageSendCOR;
using System;
using System.Collections.Generic;

namespace CMiX.MVVM.ViewModels.MessageService
{
    public class ModuleMessageDispatcher : IHandler
    {

        public ModuleMessageDispatcher()
        {
            MessageProcessors = new Dictionary<Guid, IMessageProcessor>();
        }

        public Dictionary<Guid, IMessageProcessor> MessageProcessors { get; set; }

        private IHandler _nextHandler;
        public IHandler SetNextSender(IHandler handler)
        {
            _nextHandler = handler;
            return handler;
        }


        public void SendMessage(IMessage message)
        {
            if(_nextHandler != null)
            {
                //message.ComponentID = ParentComponent.ID;
                _nextHandler.SendMessage(message);
                Console.WriteLine("ModuleMessageDispatcher SendMessage");
            }
        }


        public void RegisterMessageProcessor(IMessageProcessor messageProcessor)
        {
            if (MessageProcessors.ContainsKey(messageProcessor.ID))
                MessageProcessors[messageProcessor.ID] = messageProcessor;
            else
                MessageProcessors.Add(messageProcessor.ID, messageProcessor);
        }

        public void UnregisterMessageProcessor(IMessageProcessor messageProcessor)
        {
            MessageProcessors.Remove(messageProcessor.ID);
        }
    }
}