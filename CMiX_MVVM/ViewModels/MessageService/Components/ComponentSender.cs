using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels.Components;
using CMiX.MVVM.ViewModels.Components.Messages;
using System;

namespace CMiX.MVVM.MessageService
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

            MessagePack messagePack = new MessagePack();
            messagePack.AddMessage(message);
            this.SendMessagePack(messagePack);
            Console.WriteLine("ComponentMessageSender SendMessageAdd");
        }

        public void SendMessageRemoveComponent(Guid parentID, int index)
        {
            IMessage message = new MessageRemoveComponent(parentID, index);

            MessagePack messagePack = new MessagePack();
            messagePack.AddMessage(message);
            this.SendMessagePack(messagePack);
            Console.WriteLine("ComponentMessageSender SendMessageRemove");
        }

        public void SendMessagePack(IMessagePack messagePack)
        {
            Console.WriteLine("ComponentMessageSender SendMessageAggregator");
            MessageSender?.SendMessagePack(messagePack);
        }
    }
}