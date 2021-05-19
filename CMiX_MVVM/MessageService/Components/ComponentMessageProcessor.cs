using CMiX.MVVM.ViewModels.Components;
using CMiX.MVVM.ViewModels.Components.Messages;
using System;
using System.Linq;

namespace CMiX.MVVM.MessageService
{
    public class ComponentMessageProcessor : IMessageProcessor
    {
        public ComponentMessageProcessor(Component component)
        {
            Component = component;
        }

        private Component Component { get; set; }

        public void ProcessMessage(Message message)
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
            newComponent.SetReceiver(Component.MessageReceiver);
            Component.AddComponent(newComponent);
            Console.WriteLine("ReceiveMessageAddComponent Count is " + Component.Components.Count);
        }

        private void ReceiveMessageRemoveComponent(MessageRemoveComponent message)
        {
            Component componentToRemove = Component.Components.ElementAt(message.Index);
            Component.RemoveComponent(componentToRemove);
            Component.MessageReceiver.UnregisterMessageReceiver(componentToRemove.ID);
            Console.WriteLine("ReceiveMessageRemoveComponent Count is " + Component.Components.Count);
        }
    }
}
