using CMiX.MVVM.ViewModels.MessageService.Messages;
using System;
using System.Collections.Generic;

namespace CMiX.MVVM.ViewModels.MessageService
{
    public class ModuleReceiver : IMessageReceiver<Module>
    {
        public ModuleReceiver()
        {
            MessageCommunicators = new Dictionary<Guid, Module>();
        }

        Dictionary<Guid, Module> MessageCommunicators { get; set; }

        private Module GetMessageProcessor(Guid id)
        {
            if (MessageCommunicators.ContainsKey(id))
                return MessageCommunicators[id];
            return null;
        }

        public void RegisterReceiver(Module messageCommunicator, Guid id)
        {
            if (!MessageCommunicators.ContainsKey(id))
                MessageCommunicators.Add(id, messageCommunicator);
        }

        public void UnregisterReceiver(Guid id)
        {
            MessageCommunicators.Remove(id);
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