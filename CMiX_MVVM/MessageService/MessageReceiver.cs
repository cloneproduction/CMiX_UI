using CMiX.MVVM.ViewModels;
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

        public MessageReceiver(IIDObject obj) : this()
        {
            ID = obj.ID;
            IDObject = obj;
        }


        private Dictionary<Guid, IMessageReceiver> _messageReceivers { get; set; }
        private IIDObject IDObject { get; set; }
        private Guid ID { get; set; }

        public Guid GetID()
        {
            return ID;
        }

        private IMessageReceiver GetMessageProcessor(Guid id)
        {
            if (_messageReceivers.ContainsKey(id))
                return _messageReceivers[id];
            return null;
        }


        public void SetReceiver(IMessageReceiver messageReceiver)
        {
            messageReceiver?.RegisterReceiver(this);
        }

        public void UnsetReceiver(IMessageReceiver messageReceiver)
        {
            messageReceiver?.UnregisterReceiver(this);
        }

        public void RegisterReceiver(IMessageReceiver messageReceiver)
        {
            if (!_messageReceivers.ContainsKey(messageReceiver.GetID()))
                _messageReceivers.Add(messageReceiver.GetID(), messageReceiver);
        }

        public void UnregisterReceiver(IMessageReceiver messageReceiver)
        {
            _messageReceivers.Remove(messageReceiver.GetID());
            Console.WriteLine("MessageReceiver Count is " + _messageReceivers.Count);
        }

        public void ReceiveMessage(IIDIterator idIterator)
        {
            idIterator.Next();

            if (idIterator.IsDone)
            {
                Console.WriteLine("idIterator.IsDone");
                idIterator.Process(IDObject);
                return;
            }

            Console.WriteLine("IDIterator CurrentID is " + idIterator.CurrentID);
            GetMessageProcessor(idIterator.CurrentID).ReceiveMessage(idIterator);
        }
    }
}