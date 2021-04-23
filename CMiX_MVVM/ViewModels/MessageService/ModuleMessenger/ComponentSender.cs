using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels.Components;
using CMiX.MVVM.ViewModels.Components.Messages;
using System;

namespace CMiX.MVVM.ViewModels.MessageService
{
    public class ComponentSender : IMessageSender
    {
        public ComponentSender()
        {

        }

        private IMessageSender MessageSender;
        public IMessageSender SetSender(IMessageSender messageSender)
        {
            MessageSender = messageSender;
            return messageSender;
        }

        public void SendMessageAddComponent(Guid parentID, Component newComponent)
        {
            IMessage message = new MessageAddComponent(parentID, newComponent.GetModel() as IComponentModel);
            this.SendMessage(message);
            Console.WriteLine("ComponentMessageSender SendMessageAdd");
        }

        public void SendMessageRemoveComponent(Guid parentID, int index)
        {
            IMessage message = new MessageRemoveComponent(parentID, index);
            this.SendMessage(message);
            Console.WriteLine("ComponentMessageSender SendMessageRemove");
        }

        public void SendMessage(IMessage message)
        {
            MessageSender?.SendMessage(message);
        }
    }
}