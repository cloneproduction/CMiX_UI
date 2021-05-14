using CMiX.MVVM.ViewModels.Messages;
using System;

namespace CMiX.MVVM.ViewModels.MessageService.ModuleMessenger
{
    public class ComponentManagerMessageSender : IMessageSender
    {
        public ComponentManagerMessageSender()
        {

        }

        private IMessageSender MessageSender;
        public IMessageSender SetSender(IMessageSender messageSender)
        {
            MessageSender = messageSender;
            return messageSender;
        }

        public void SendMessagePack(IMessagePack messagePack)
        {
            if (MessageSender != null)
            {
                MessageSender.SendMessagePack(messagePack);
                Console.WriteLine("ManagerMessageSender SendMessageAggregator");
            }
        }
    }
}