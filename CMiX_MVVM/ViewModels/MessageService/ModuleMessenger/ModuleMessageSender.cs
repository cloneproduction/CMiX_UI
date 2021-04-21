using CMiX.MVVM.ViewModels.MessageService.Messages;
using System;

namespace CMiX.MVVM.ViewModels.MessageService.ModuleMessenger
{
    public class ModuleMessageSender : IMessageDispatcher, IMessageSender
    {
        public ModuleMessageSender(Guid componentID)
        {
            ComponentID = componentID;
        }


        Guid ComponentID { get; set; }

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

        public void SendMessageUpdateViewModel(Module module)
        {
            MessageSender?.SendMessage(new MessageUpdateViewModel(module.ID, module.GetModel()));
        }


        public void SendMessage(IMessage message)
        {
            if (MessageSender != null)
            {
                message.ComponentID = ComponentID;
                MessageSender.SendMessage(message);
                Console.WriteLine("ModuleMessageSender SendMessage");
            }
        }
    }
}