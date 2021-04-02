using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.MessageService.Messages;
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

        public void OnMessageOutNotification(IMessage message)
        {
            var handler = MessageOutNotification;
            if (handler != null)
            {
                handler(message);
            }
        }

        public void DispatchMessage(IMessage message)
        {
            IMessageProcessor messageProcessor;

            if (MessageProcessors.TryGetValue(message.ComponentID, out messageProcessor))
            {
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


        public void Dispose()
        {
            MessageProcessors.Clear();
        }
    }
}