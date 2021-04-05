using CMiX.MVVM.ViewModels.Components.Messages;
using CMiX.MVVM.ViewModels.MessageService.MessageSendCOR;
using System;
using System.Collections.Generic;

namespace CMiX.MVVM.ViewModels.MessageService
{
    public class MessageDispatcher : IMessageDispatcher 
    {
        public MessageDispatcher()
        {
            MessageProcessors = new Dictionary<Guid, IMessageProcessor>();
        }



        public Dictionary<Guid, IMessageProcessor> MessageProcessors { get; set; }

        public event Action<IMessage> MessageOutNotification;
        public void OnMessageOutNotification(IMessage message)
        {
            var handler = MessageOutNotification;
            if (handler != null)
            {
                handler(message);
            }
        }

        public bool CanSend()
        {
            var handler = MessageOutNotification;
            if (handler != null)
            {
                return true;
            }
            return false;
        }




        public event Action<IMessage> MessageInNotification;
        public void OnMessageInNotification(IMessage message)
        {
            //this.ProcessMessage(message);
            var handler = MessageInNotification;
            if (handler != null)
            {
                handler(message);
            }
        }


        public IMessageProcessor GetMessageProcessor(Guid id)
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
                OnMessageInNotification(message);
            }
            else
            {
                var messageProcessor = GetMessageProcessor(message.ComponentID);
                if(messageProcessor != null)
                    messageProcessor.ProcessMessage(message);
            }
        }


        public void RegisterMessageProcessor(IMessageProcessor messageProcessor)
        {
            messageProcessor.MessageOutNotification += OnMessageOutNotification;

            if (MessageProcessors.ContainsKey(messageProcessor.ID))
                MessageProcessors[messageProcessor.ID] = messageProcessor;
            else
                MessageProcessors.Add(messageProcessor.ID, messageProcessor);
        }


        public void UnregisterMessageProcessor(IMessageProcessor messageProcessor)
        {
            MessageProcessors.Remove(messageProcessor.ID);
        }

        public void SendMessage(Func<IMessage> CreateMessage)
        {
            var message = CreateMessage();
            OnMessageOutNotification(message);
        }



        private IHandler _nextHandler;
        public IHandler SetNext(IHandler handler)
        {
            _nextHandler = handler;
            return handler;
        }

        public void SendMessage(IMessage message)
        {
            if(_nextHandler != null)
            {
                _nextHandler.SendMessage(message);
            }
        }
    }
}