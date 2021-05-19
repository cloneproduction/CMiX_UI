using CMiX.MVVM.ViewModels;
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

        public MessageReceiver(IIDobject iDobject)
        {
            ID = iDobject.ID;
            IDobject = iDobject;
            _messageReceivers = new Dictionary<Guid, IMessageReceiver>();
        }


        private Guid ID { get; set; }
        private IIDobject IDobject { get; set; }
        private Dictionary<Guid, IMessageReceiver> _messageReceivers { get; set; }

        public Guid GetID()
        {
            return IDobject.ID;
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
                IDobject?.MessageProcessor.ProcessMessage(idIterator.Message);
                return;
            }

            IMessageReceiver messageReceiver = GetMessageProcessor(idIterator.CurrentID);

            if (messageReceiver != null)
                messageReceiver.ReceiveMessage(idIterator);
        }
    }
}