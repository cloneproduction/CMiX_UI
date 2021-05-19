using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels.Components;
using CMiX.MVVM.ViewModels.Components.Messages;
using System;

namespace CMiX.MVVM.MessageService.Components
{
    public class ComponentMessageEmitter : IMessageEmitter
    {
        public ComponentMessageEmitter(IMessageSender messageSender)
        {
            MessageSender = messageSender;
        }

        private IMessageSender MessageSender { get; set; }

        public void SendMessageAddComponent(Component newComponent)
        {
            Console.WriteLine("ComponentMessageSender SendMessageAdd");
            Message message = new MessageAddComponent(newComponent.GetModel() as IComponentModel);
            MessageSender.SendMessage(message);
        }

        public void SendMessageRemoveComponent(int index)
        {
            Console.WriteLine("ComponentMessageSender SendMessageRemove");
            Message message = new MessageRemoveComponent(index);
            MessageSender.SendMessage(message);
        }
    }
}