using CMiX.MVVM.ViewModels.MessageService.Messages;
using System;
using System.Collections.Generic;

namespace CMiX.MVVM.ViewModels.MessageService
{
    public class ModuleMessageReceiver : IMessageReceiver, IMessageDispatcher
    {
        public ModuleMessageReceiver()
        {
            MessageCommunicators = new Dictionary<Guid, IMessageCommunicator>();
        }

        private Dictionary<Guid, IMessageCommunicator> MessageCommunicators { get; set; }

        private IMessageCommunicator GetMessageProcessor(Guid id)
        {
            if (MessageCommunicators.ContainsKey(id))
                return MessageCommunicators[id];
            return null;
        }


        public void RegisterReceiver(IMessageCommunicator messageCommunicator)
        {
            if (MessageCommunicators.ContainsKey(messageCommunicator.ID))
                MessageCommunicators[messageCommunicator.ID] = messageCommunicator;
            else
                MessageCommunicators.Add(messageCommunicator.ID, messageCommunicator);
        }

        public void UnregisterReceiver(IMessageCommunicator messageCommunicator)
        {
            MessageCommunicators.Remove(messageCommunicator.ID);
        }


        public void ProcessMessage(IMessage message)
        {
            var msg = message as MessageUpdateViewModel;
            var module = GetMessageProcessor(msg.ModuleID);

            if (msg != null && module != null)
            {
                module.ReceiveMessage(msg);
            }
        }
    }
}