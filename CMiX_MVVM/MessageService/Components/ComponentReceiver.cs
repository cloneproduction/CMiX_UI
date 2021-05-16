using System;
using System.Collections.Generic;

namespace CMiX.MVVM.MessageService
{
    public class MessageReceiver : IMessageReceiver
    {
        public MessageReceiver()
        {
            MessageProcessors = new Dictionary<Guid, IMessageProcessor>();
        }

        Dictionary<Guid, IMessageProcessor> MessageProcessors { get; set; }


        private IMessageProcessor GetMessageProcessor(Guid id)
        {
            if (MessageProcessors.ContainsKey(id))
                return MessageProcessors[id];
            return null;
        }

        public void RegisterReceiver(Guid id, IMessageProcessor component)
        {
            if (!MessageProcessors.ContainsKey(id))
                MessageProcessors.Add(id, component);
        }

        public void UnregisterReceiver(Guid id)
        {
            MessageProcessors.Remove(id);
        }


        public void ReceiveMessage(IMessageIterator messageIterator)
        {
            IMessage message = messageIterator.Next();

            if (message != null)
            {
                IMessageProcessor messageProcessor = GetMessageProcessor(message.ComponentID);

                if (messageProcessor == null)
                    return;

                messageProcessor.ProcessMessage(message);

                if (!messageIterator.IsDone)
                    messageProcessor.DispatchIterator(messageIterator);
            }
        }
    }
}