using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels.Components;
using CMiX.MVVM.ViewModels.Components.Messages;
using System;

namespace CMiX.MVVM.ViewModels.MessageService
{
    public class ComponentMessageSender : IMessageSender, IMessageDispatcher
    {
        public ComponentMessageSender()
        {

        }


        private IMessageSender MessageSender;
        public IMessageSender SetSender(IMessageSender messageSender)
        {
            MessageSender = messageSender;
            return messageSender;
        }


        public void SendMessageAddComponent(Component component, Component newComponent)
        {
            MessageSender.SendMessage(new MessageAddComponent(component.ID, newComponent.GetModel() as IComponentModel));
            Console.WriteLine("ManagerMessageSender SendMessageAdd");
        }

        public void SendMessageRemoveComponent(Component selectedParent, int index)
        {
            MessageSender?.SendMessage(new MessageRemoveComponent(selectedParent.ID, index));
            Console.WriteLine("ManagerMessageSender SendMessageRemove");
        }

        public void SendMessage(IMessage message)
        {
            MessageSender?.SendMessage(message);
        }
    }
}