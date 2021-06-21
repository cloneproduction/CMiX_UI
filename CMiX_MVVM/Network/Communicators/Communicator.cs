// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using CMiX.Core.Network.Messages;
using CMiX.Core.Presentation.ViewModels;
using System;
using System.Collections.Generic;

namespace CMiX.Core.Network.Communicators
{
    public class Communicator
    {
        public Communicator()
        {
            _childCommunicator = new Dictionary<Guid, Communicator>();
            CanSend = true;
        }


        internal Communicator _parentCommunicator { get; set; }
        internal Dictionary<Guid, Communicator> _childCommunicator { get; set; }
        internal IIDObject IIDObject { get; set; }

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
            Console.WriteLine("MessageReceiver Count is " + _childCommunicator.Count);
        }


        private Communicator GetMessageProcessor(Guid id)
        {
            if (_childCommunicator.ContainsKey(id))
                return _childCommunicator[id];
            return null;
        }

        public void ReceiveMessage(IIDIterator idIterator)
        {
            idIterator.Next();

            if (idIterator.IsDone)
            {
                Console.WriteLine("idIterator.IsDone on " + IIDObject.GetType());
                this.CanSend = false;
                idIterator.Process(IIDObject);
                return;
            }

            Console.WriteLine("IDIterator CurrentID is " + idIterator.CurrentID + " " + IIDObject.GetType());
            GetMessageProcessor(idIterator.CurrentID).ReceiveMessage(idIterator);
            this.CanSend = true;
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
        public virtual void SendMessage(Message message)
        {
            if (CanSend)
            {
                Console.WriteLine("Communicator SendMessage with ID " + GetID());
                message.AddID(GetID());
                _parentCommunicator?.SendMessage(message);
            }
        }
    }
}
