using CMiX.MVVM.ViewModels.Components;
using CMiX.MVVM.ViewModels.Components.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.MVVM.ViewModels.MessageService.ModuleMessenger
{
    public class ComponentMessageProcessor
    {
        public ComponentMessageProcessor()
        {

        }

        public void ProcessMessage(IMessage message, Component component)
        {
            if (component == null)
                return;

            if (message is MessageAddComponent)
            {
                ReceiveMessageAddComponent(component, message as MessageAddComponent);
                return;
            }

            if (message is MessageRemoveComponent)
            {
                ReceiveMessageRemoveComponent(component, message as MessageRemoveComponent);
                return;
            }

            component.MessageReceiver.ReceiveMessage(message);
        }

        private void ReceiveMessageAddComponent(Component component, MessageAddComponent message)
        {
            Component newComponent = component.ComponentFactory.CreateComponent(message.ComponentModel);
            newComponent.SetReceiver(component.ComponentReceiver);
            component.AddComponent(newComponent);
            Console.WriteLine("ComponentReceiver ReceiveMessageAddComponent Count is " + component.Components.Count);
        }

        private void ReceiveMessageRemoveComponent(Component component, MessageRemoveComponent message)
        {
            Component componentToRemove = component.Components.ElementAt(message.Index);
            component.RemoveComponent(componentToRemove);
            component.ComponentReceiver.UnregisterReceiver(componentToRemove.ID);
            Console.WriteLine("ComponentReceiver ReceiveMessageRemoveComponent Count is " + component.Components.Count);
        }

    }
}
