using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels.Components;
using CMiX.MVVM.ViewModels.Components.Messages;
using System;

namespace CMiX.MVVM.ViewModels.MessageService
{
    public class ComponentMessageSender : IMessageSender
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

        public void SendMessageAddComponent(Guid parentID, Component newComponent)
        {
            this.SendMessage(new MessageAddComponent(parentID, newComponent.GetModel() as IComponentModel));
            Console.WriteLine("ManagerMessageSender SendMessageAdd");
        }

        public void SendMessageRemoveComponent(Guid parentID, int index)
        {
            this.SendMessage(new MessageRemoveComponent(parentID, index));
            Console.WriteLine("ManagerMessageSender SendMessageRemove");
        }

        public void SendMessage(IMessage message)
        {
            MessageSender?.SendMessage(message);
        }
    }
}