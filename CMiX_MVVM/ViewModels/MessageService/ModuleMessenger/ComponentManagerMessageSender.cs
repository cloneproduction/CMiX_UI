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

        public void SendMessage(IMessage message)
        {
            if (MessageSender != null)
            {
                MessageSender.SendMessage(message);
                Console.WriteLine("ManagerMessageSender SendMessage");
            }
        }
    }
}