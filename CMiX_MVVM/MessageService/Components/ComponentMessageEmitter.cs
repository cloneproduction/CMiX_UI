using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels.Components;
using CMiX.MVVM.ViewModels.Components.Messages;
using System;

namespace CMiX.MVVM.MessageService.Components
{
    public class ComponentMessageEmitter : IMessageEmitter
    {
        public ComponentMessageEmitter()
        {

        }

        public IMessageSender MessageSender { get; set; }

        public void SetSender(IMessageSender messageSender)
        {
            MessageSender = messageSender;
        }

        public void SendMessageAddComponent(Guid id, Component newComponent)
        {
            Message message = new MessageAddComponent(id, newComponent.GetModel() as IComponentModel);
            MessageSender.SendMessage(message);
            Console.WriteLine("ComponentMessageSender SendMessageAdd");
        }

        public void SendMessageRemoveComponent(Guid id, int index)
        {
            Message message = new MessageRemoveComponent(id, index);
            MessageSender.SendMessage(message);
            Console.WriteLine("ComponentMessageSender SendMessageRemove");
        }
    }
}
