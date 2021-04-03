using System;
using System.Collections.Generic;

namespace CMiX.MVVM.ViewModels.MessageService
{
    public class MessageDispatcher : IDisposable, IMessageDispatcher
    {
        public MessageDispatcher()
        {
            MessageProcessors = new Dictionary<Guid, IMessageProcessor>();
        }

        public Guid ID { get; set; }
        public Dictionary<Guid, IMessageProcessor> MessageProcessors { get; set; }


        public event Action<IMessage> MessageOutNotification;
        public event Action<IMessage> MessageInNotification;

        public void OnMessageOutNotification(IMessage message)
        {
            var handler = MessageOutNotification;
            if (handler != null)
            {
                handler(message);
            }
        }


        public void OnMessageInNotification(IMessage message)
        {
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


        public void Dispose()
        {
            MessageProcessors.Clear();
        }
    }
}