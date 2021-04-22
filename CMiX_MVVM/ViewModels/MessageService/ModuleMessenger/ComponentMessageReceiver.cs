using CMiX.MVVM.ViewModels.Components;
using CMiX.MVVM.ViewModels.Components.Messages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CMiX.MVVM.ViewModels.MessageService.ModuleMessenger
{
    public class ComponentMessageReceiver : IMessageReceiver//, IMessageDispatcher
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

        public void ReceiveMessage(IMessage message)
        {
            var component = GetMessageProcessor(message.ComponentID) as Component;

            if (component == null)
                return;

            if (message is MessageAddComponent)
            {
                ReceiveMessageAddComponent(component, message as MessageAddComponent);
                return;
            }

            if(message is MessageRemoveComponent)
            {
                ReceiveMessageRemoveComponent(component, message as MessageRemoveComponent);
                return;
            }

            component.MessageReceiver.ReceiveMessage(message);
        }

        private void ReceiveMessageAddComponent(Component component, MessageAddComponent message)
        {
            Component newComponent = component.ComponentFactory.CreateComponent(message.ComponentModel);
            newComponent.SetReceiver(this);
            component.AddComponent(newComponent);
        }

        private void ReceiveMessageRemoveComponent(Component component, MessageRemoveComponent message)
        {
            Component componentToRemove = component.Components.ElementAt(message.Index);
            component.RemoveComponent(componentToRemove);
            this.UnregisterReceiver(componentToRemove);
        }
    }
}