using System;
using System.Collections.Generic;

namespace CMiX.MVVM.MessageService
{
    public class MessageReceiver : IMessageReceiver
    {
        public MessageReceiver()
        {
            _messageReceivers = new Dictionary<Guid, IMessageReceiver>();
        }

        public MessageReceiver(IMessageProcessor messageProcessor)
        {
            MessageProcessor = messageProcessor;
            _messageReceivers = new Dictionary<Guid, IMessageReceiver>();
        }


        private IMessageProcessor MessageProcessor { get; set; }
        private Dictionary<Guid, IMessageReceiver> _messageReceivers { get; set; }

        private IMessageReceiver GetMessageProcessor(Guid id)
        {
            if (_messageReceivers.ContainsKey(id))
                return _messageReceivers[id];
            return null;
        }

        public void RegisterMessageReceiver(Guid id, IMessageReceiver receiver)
        {
            if (!_messageReceivers.ContainsKey(id))
                _messageReceivers.Add(id, receiver);
        }

        public void UnregisterMessageReceiver(Guid id)
        {
            _messageReceivers.Remove(id);
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