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

        public void RegisterMessageProcessor(Guid id, IMessageProcessor component)
        {
            if (!MessageProcessors.ContainsKey(id))
                MessageProcessors.Add(id, component);
        }

        public void UnregisterMessageProcessor(Guid id)
        {
            MessageProcessors.Remove(id);
        }

        public void ReceiveMessage(IIDIterator idIterator)
        {
            idIterator.Next();
            Guid id = idIterator.CurrentID;

            IMessageProcessor messageProcessor = GetMessageProcessor(id);

            if (messageProcessor == null)
                return;

            messageProcessor.ProcessMessage(idIterator.Message);

            if (!idIterator.IsDone)
                messageProcessor.DispatchIterator(idIterator);
        }
    }
}