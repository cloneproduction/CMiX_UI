using CMiX.MVVM.ViewModels.Components;
using CMiX.MVVM.ViewModels.Components.Messages;
using CMiX.MVVM.ViewModels.Messages;
using System;
using System.Linq;

namespace CMiX.MVVM.ViewModels.MessageService.ModuleMessenger
{
    public class ComponentMessageProcessor : IMessageProcessor
    {
        public ComponentMessageProcessor(Component component, MessageReceiver messageReceiver)
        {
            Component = component;
            ComponentReceiver = messageReceiver;
        }

        private MessageReceiver ComponentReceiver { get; set; }
        private Component Component { get; set; }

        public void DispatchIterator(IMessageIterator messageIterator)
        {
            Component.MessageReceiver.ReceiveMessage(messageIterator);
        }


        public void ProcessMessage(IMessage message)
        {
            if (message is MessageAddComponent)
            {
                ReceiveMessageAddComponent(message as MessageAddComponent);
                return;
            }

            if (message is MessageRemoveComponent)
            {
                ReceiveMessageRemoveComponent(message as MessageRemoveComponent);
                return;
            }
        }

        private void ReceiveMessageAddComponent(MessageAddComponent message)
        {
            Component newComponent = Component.ComponentFactory.CreateComponent(message.ComponentModel);
            newComponent.SetReceiver(ComponentReceiver);
            Component.AddComponent(newComponent);
            Console.WriteLine("ComponentReceiver ReceiveMessageAddComponent Count is " + Component.Components.Count);
        }

        private void ReceiveMessageRemoveComponent(MessageRemoveComponent message)
        {
            Component componentToRemove = Component.Components.ElementAt(message.Index);
            Component.RemoveComponent(componentToRemove);
            ComponentReceiver.UnregisterReceiver(componentToRemove.ID);
            Console.WriteLine("ComponentReceiver ReceiveMessageRemoveComponent Count is " + Component.Components.Count);
        }
    }
}
