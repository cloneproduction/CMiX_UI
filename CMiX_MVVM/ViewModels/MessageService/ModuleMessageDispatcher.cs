using CMiX.MVVM.ViewModels.MessageService.MessageSendCOR;
using System;
using System.Collections.Generic;

namespace CMiX.MVVM.ViewModels.MessageService
{
    public class ModuleMessageDispatcher : IMessageSendHandler, IMessageReceiveHandler
    {

        public ModuleMessageDispatcher()
        {
            MessageProcessors = new Dictionary<Guid, IMessageReceiveHandler>();
        }


        public Guid ID { get; set; }

        private IMessageSendHandler _nextHandler;
        public IMessageSendHandler SetNextSender(IMessageSendHandler handler)
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

        public Dictionary<Guid, IMessageReceiveHandler> MessageProcessors { get; set; }

        public void RegisterMessageProcessor(IMessageReceiveHandler messageProcessor)
        {
            if (MessageProcessors.ContainsKey(messageProcessor.ID))
                MessageProcessors[messageProcessor.ID] = messageProcessor;
            else
                MessageProcessors.Add(messageProcessor.ID, messageProcessor);
        }

        public void UnregisterMessageProcessor(IMessageReceiveHandler messageProcessor)
        {
            MessageProcessors.Remove(messageProcessor.ID);
        }

        public void ReceiveMessage(IMessage message)
        {
            throw new NotImplementedException();
        }
    }
}