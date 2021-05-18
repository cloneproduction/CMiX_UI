using System;
using System.Collections.Generic;

namespace CMiX.MVVM.MessageService
{
    public class MessageReceiver : IMessageReceiver
    {
        public MessageReceiver()
        {
            MessageProcessors = new Dictionary<Guid, IMessageReceiver>();
        }

        public MessageReceiver(IMessageProcessor messageProcessor)
        {
            MessageProcessor = messageProcessor;
            MessageProcessors = new Dictionary<Guid, IMessageReceiver>();
        }


        private IMessageProcessor MessageProcessor { get; set; }
        private Dictionary<Guid, IMessageReceiver> MessageProcessors { get; set; }

        private IMessageReceiver GetMessageProcessor(Guid id)
        {
            if (MessageProcessors.ContainsKey(id))
                return MessageProcessors[id];
            return null;
        }

        public void RegisterMessageReceiver(Guid id, IMessageReceiver component)
        {
            if (!MessageProcessors.ContainsKey(id))
                MessageProcessors.Add(id, component);
        }

        public void UnregisterMessageReceiver(Guid id)
        {
            MessageProcessors.Remove(id);
        }

        public void ReceiveMessage(IIDIterator idIterator)
        {
            idIterator.Next();

            if (idIterator.IsDone)
            {
                MessageProcessor.ProcessMessage(idIterator.Message);
                return;
            }

            IMessageReceiver messageReceiver = GetMessageProcessor(idIterator.CurrentID);

            if (messageReceiver != null)
                messageReceiver.ReceiveMessage(idIterator);
        }
    }
}