using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels.Components;
using CMiX.MVVM.ViewModels.Components.Messages;
using System;

namespace CMiX.MVVM.ViewModels.MessageService.ModuleMessenger
{
    public class ComponentMessageSender : IMessageSender, IMessageDispatcher
    {
        public ComponentMessageSender()
        {

        }

        public IMessage SetMessageID(IMessage message)
        {
            return message;
        }

        private IMessageSender MessageSender;
        public IMessageSender SetSender(IMessageSender messageSender)
        {
            MessageSender = messageSender;
            return messageSender;
        }

        public void ProcessMessage(IMessage message)
        {
            if (MessageSender != null)
            {
                MessageSender.ProcessMessage(message);
                Console.WriteLine("ComponentMessageSender SendMessage");
            }
        }

        public void SendMessageAddComponent(Component component, Component newComponent)
        {
            if (MessageSender != null)
            {
                MessageSender.ProcessMessage(new MessageAddComponent(component.ID, newComponent.GetModel() as IComponentModel));
                Console.WriteLine("ManagerMessageSender SendMessageAdd");
            }
        }

        public void SendMessageRemoveComponent(Component selectedParent, int index)
        {
            if (MessageSender != null)
            {
                MessageSender.ProcessMessage(new MessageRemoveComponent(selectedParent.ID, index));
                Console.WriteLine("ManagerMessageSender SendMessageRemove");
            }
        }
    }
}