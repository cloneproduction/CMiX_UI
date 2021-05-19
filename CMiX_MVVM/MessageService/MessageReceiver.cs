using System;
using System.Collections.Generic;

namespace CMiX.MVVM.MessageService
{
    public class MessageReceiver : IMessageReceiver
    {
        public MessageReceiver(Guid id)
        {
            ID = id;
            _messageReceivers = new Dictionary<Guid, IMessageReceiver>();
        }

        public MessageReceiver(IMessageProcessor messageProcessor)
        {
            ID = messageProcessor.GetID();
            MessageProcessor = messageProcessor;
            _messageReceivers = new Dictionary<Guid, IMessageReceiver>();
        }


        public Guid ID { get; set; }
        private IMessageProcessor MessageProcessor { get; set; }
        private Dictionary<Guid, IMessageReceiver> _messageReceivers { get; set; }


        private IMessageReceiver GetMessageProcessor(Guid id)
        {
            if (_messageReceivers.ContainsKey(id))
                return _messageReceivers[id];
            return null;
        }

        public void RegisterMessageReceiver(IMessageReceiver receiver)
        {
            if (!_messageReceivers.ContainsKey(receiver.ID))
                _messageReceivers.Add(receiver.ID, receiver);
        }

        public void UnregisterMessageReceiver(IMessageReceiver receiver)
        {
            _messageReceivers.Remove(receiver.ID);
        }

        public void ReceiveMessage(IIDIterator idIterator)
        {
            idIterator.Next();

            if (idIterator.IsDone)
            {
                MessageProcessor?.ProcessMessage(idIterator.Message);
                return;
            }

            IMessageReceiver messageReceiver = GetMessageProcessor(idIterator.CurrentID);

            if (messageReceiver != null)
                messageReceiver.ReceiveMessage(idIterator);
        }
    }
}