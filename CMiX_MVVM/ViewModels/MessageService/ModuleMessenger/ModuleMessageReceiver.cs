using CMiX.MVVM.ViewModels.MessageService.Messages;
using System;
using System.Collections.Generic;

namespace CMiX.MVVM.ViewModels.MessageService
{
    public class ModuleMessageReceiver : IMessageReceiver
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
            if (!MessageCommunicators.ContainsKey(messageCommunicator.ID))
                MessageCommunicators[messageCommunicator.ID] = messageCommunicator;

        }


        public void UnregisterReceiver(IMessageCommunicator messageCommunicator)
        {
            MessageCommunicators.Remove(messageCommunicator.ID);
        }


        public void ReceiveMessage(IMessage message)
        {
            var msg = message as MessageUpdateViewModel;
            Module module = null;

            if(msg != null)
                module = GetMessageProcessor(msg.ModuleID) as Module;

            if (msg != null && module != null)
                module.SetViewModel(msg.Model);
        }
    }
}