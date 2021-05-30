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
            _messageReceivers = new Dictionary<Guid, IMessageReceiver>();
            _messageProcessor = messageProcessor;
        }


        private Dictionary<Guid, IMessageReceiver> _messageReceivers { get; set; }
        private IMessageProcessor _messageProcessor { get; set; }


        public Guid GetID()
        {
            return _messageProcessor.GetID();
        }

        private IMessageReceiver GetMessageProcessor(Guid id)
        {
            if (_messageReceivers.ContainsKey(id))
                return _messageReceivers[id];
            return null;
        }

        public void RegisterReceiver(IMessageReceiver receiver)
        {
            if (!_messageReceivers.ContainsKey(receiver.GetID()))
                _messageReceivers.Add(receiver.GetID(), receiver);
        }

        public void UnregisterReceiver(IMessageReceiver receiver)
        {
            _messageReceivers.Remove(receiver.GetID());
        }

        public void ReceiveMessage(IIDIterator idIterator)
        {
            idIterator.Next();
            
            if (idIterator.IsDone)
            {
                Console.WriteLine("idIterator.IsDone");
                _messageProcessor.ProcessMessage(idIterator.Message);
                return;
            }

            Console.WriteLine("IDIterator CurrentID is " + idIterator.CurrentID);
            IMessageReceiver messageReceiver = GetMessageProcessor(idIterator.CurrentID);

            if (messageReceiver != null)
                messageReceiver.ReceiveMessage(idIterator);
        }
    }
}