using CMiX.MVVM.ViewModels.Components;
using CMiX.MVVM.ViewModels.Components.Messages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CMiX.MVVM.ViewModels.MessageService.ModuleMessenger
{
    public class ComponentReceiver : IMessageReceiver<Component>
    {
        public ComponentReceiver()
        {
            MessageCommunicators = new Dictionary<Guid, Component>();
        }

        Dictionary<Guid, Component> MessageCommunicators { get; set; }

        public Component GetMessageProcessor(Guid id)
        {
            if (MessageCommunicators.ContainsKey(id))
                return MessageCommunicators[id];
            return null;
        }

        public void RegisterReceiver(Component component, Guid id)
        {
            if (!MessageCommunicators.ContainsKey(id))
                MessageCommunicators.Add(id, component);
        }

        public void UnregisterReceiver(Guid id)
        {
            MessageCommunicators.Remove(id);
        }


        public void ReceiveMessage(IMessage message)
        {
            var component = GetMessageProcessor(message.ComponentID);

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
            Console.WriteLine("ComponentReceiver ReceiveMessageAddComponent Count is " + component.Components.Count);
        }

        private void ReceiveMessageRemoveComponent(Component component, MessageRemoveComponent message)
        {
            Component componentToRemove = component.Components.ElementAt(message.Index);
            component.RemoveComponent(componentToRemove);
            this.UnregisterReceiver(componentToRemove.ID);
            Console.WriteLine("ComponentReceiver ReceiveMessageRemoveComponent Count is " + component.Components.Count);
        }
    }
}