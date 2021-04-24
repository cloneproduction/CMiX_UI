using CMiX.MVVM.ViewModels.MessageService.Messages;
using System;

namespace CMiX.MVVM.ViewModels.MessageService.ModuleMessenger
{
    public class ModuleSender : IMessageSender
    {
        public ModuleSender(Guid componentID)
        {
            ComponentID = componentID;
        }

        Guid ComponentID { get; set; }

        private IMessageSender MessageSender;
        public IMessageSender SetSender(IMessageSender messageSender)
        {
            MessageSender = messageSender;
            return messageSender;
        }

        public void SendMessageUpdateViewModel(Module module)
        {
            this.SendMessage(new MessageUpdateViewModel(module.ID, module.GetModel()));
        }

        public void SendMessage(IMessage message)
        {
            if (MessageSender != null)
            {
                message.ComponentID = ComponentID;
                MessageSender.SendMessage(message);
                Console.WriteLine("ModuleSender SendMessage ComponentAddress" + message.ComponentID);
            }
        }
    }
}