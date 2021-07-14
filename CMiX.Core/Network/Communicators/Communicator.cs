// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.Collections.Generic;
using CMiX.Core.Network.Messages;
using CMiX.Core.Presentation.ViewModels;

namespace CMiX.Core.Network.Communicators
{
    public class Communicator
    {
        public Communicator(IIDObject iDObject)
        {
            IIDObject = iDObject;
            _childCommunicator = new Dictionary<Guid, Communicator>();
            CanSend = true;
        }


        public event EventHandler<MessageEventArgs> MessageReceived;
        protected virtual void OnMessageReceived(MessageEventArgs e)
        {
            EventHandler<MessageEventArgs> handler = MessageReceived;
            handler?.Invoke(this, e);
        }

        public event EventHandler<MessageEventArgs> MessageSent;
        protected virtual void OnMessageSent(MessageEventArgs e)
        {
            EventHandler<MessageEventArgs> handler = MessageSent;
            handler?.Invoke(this, e);
        }


        internal Communicator _parentCommunicator { get; set; }
        internal Dictionary<Guid, Communicator> _childCommunicator { get; set; }
        internal IIDObject IIDObject { get; set; }


        private Communicator GetMessageProcessor(Guid id)
        {
            if (_childCommunicator.ContainsKey(id))
                return _childCommunicator[id];
            return null;
        }

        private Guid GetID()
        {
            return IIDObject.ID;
        }


        private void RegisterReceiver(Communicator communicator)
        {
            if (!_childCommunicator.ContainsKey(communicator.GetID()))
                _childCommunicator.Add(communicator.GetID(), communicator);
        }

        private void UnregisterReceiver(Communicator communicator)
        {
            _childCommunicator.Remove(communicator.GetID());
        }


        public void ReceiveMessage(IIDIterator idIterator)
        {
            //MessageReceived.Invoke(this, new MessageEventArgs(idIterator.me));

            idIterator.Next();

            if (idIterator.IsDone)
            {
                Console.WriteLine("idIterator.IsDone on " + IIDObject.GetType());
                this.CanSend = false;
                Console.WriteLine("Communicator " + this.GetType().Name + "CanSend " + CanSend);
                idIterator.Process(IIDObject);
                return;
            }

            Console.WriteLine("IDIterator CurrentID is " + idIterator.CurrentID + " " + IIDObject.GetType());
            GetMessageProcessor(idIterator.CurrentID).ReceiveMessage(idIterator);
            this.CanSend = true;
        }

        public virtual void SendMessage(Message message)
        {
            MessageSent?.Invoke(this, new MessageEventArgs(message));

            if (CanSend)
            {
                Console.WriteLine("Communicator from" + IIDObject.GetType().Name + " SendMessage with ID " + GetID());
                message.AddID(GetID());
                _parentCommunicator?.SendMessage(message);
            }
        }


        public void SetCommunicator(Communicator communicator)
        {
            _parentCommunicator = communicator;
            communicator.RegisterReceiver(this);
        }

        public void UnsetCommunicator(Communicator communicator)
        {
            this.UnregisterReceiver(communicator);
        }


        private bool CanSend { get; set; }
    }
}
