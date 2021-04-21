using System;
using System.Collections.Generic;

namespace CMiX.MVVM.ViewModels.MessageService.ModuleMessenger
{
    public class ComponentMessageReceiver : IMessageReceiver, IMessageDispatcher
    {
        public ComponentMessageReceiver()
        {
            MessageCommunicators = new Dictionary<Guid, IMessageCommunicator>();
        }

        private Dictionary<Guid, IMessageCommunicator> MessageCommunicators { get; set; }

        public IMessageCommunicator GetMessageProcessor(Guid id)
        {
            if (MessageCommunicators.ContainsKey(id))
                return MessageCommunicators[id];
            return null;
        }

        public void RegisterReceiver(IMessageCommunicator component)
        {
            if (MessageCommunicators.ContainsKey(component.ID))
                MessageCommunicators[component.ID] = component;
            else
                MessageCommunicators.Add(component.ID, component);
        }

        public void UnregisterReceiver(IMessageCommunicator component)
        {
            MessageCommunicators.Remove(component.ID);
        }

        public void ProcessMessage(IMessage message)
        {
            var component = GetMessageProcessor(message.ComponentID);
            if(component != null)
            {
                component.ReceiveMessage(message);
            }
        }
    }
}