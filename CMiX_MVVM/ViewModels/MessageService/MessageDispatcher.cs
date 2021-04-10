using CMiX.MVVM.ViewModels.Components.Messages;
using CMiX.MVVM.ViewModels.MessageService.MessageSendCOR;
using System;
using System.Collections.Generic;

namespace CMiX.MVVM.ViewModels.MessageService
{
    public class MessageDispatcher //: IMessageDispatcher 
    {
        public MessageDispatcher()
        {
            MessageProcessors = new Dictionary<Guid, IMessageReceiveHandler>();
        }


        public Dictionary<Guid, IMessageReceiveHandler> MessageProcessors { get; set; }


        public IMessageReceiveHandler GetMessageProcessor(Guid id)
        {
            if (MessageProcessors.ContainsKey(id))
                return MessageProcessors[id];
            return null;
        }


        public IMessage SetMessageID(IMessage message )
        {
            return message;
        }


        public void ProcessMessage(IMessage message)
        {
            if (message is IComponentManagerMessage)
            {
                ///OnMessageInNotification(message);
            }
            else
            {
                var messageProcessor = GetMessageProcessor(message.ComponentID);
                if(messageProcessor != null)
                    messageProcessor.ReceiveMessage(message);
            }
        }


        public void RegisterMessageProcessor(IMessageReceiveHandler messageProcessor)
        {
            //messageProcessor.MessageOutNotification += OnMessageOutNotification;

            if (MessageProcessors.ContainsKey(messageProcessor.ID))
                MessageProcessors[messageProcessor.ID] = messageProcessor;
            else
                MessageProcessors.Add(messageProcessor.ID, messageProcessor);
        }


        public void UnregisterMessageProcessor(IMessageProcessor messageProcessor)
        {
            MessageProcessors.Remove(messageProcessor.ID);
        }


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
                _nextHandler.SendMessage(message);
                Console.WriteLine("MessageDispatcher SendMessage");
            }
        }
    }
}