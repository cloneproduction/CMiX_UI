using CMiX.MVVM.ViewModels;
using System;

namespace CMiX.MVVM.MessageService.Module
{
    public class ControlMessageEmitter : IMessageEmitter
    {
        public ControlMessageEmitter(IMessageSender messageSender)
        {
            MessageSender = messageSender;
        }

        IMessageSender MessageSender { get; set; }

        public IMessageSender GetMessageSender()
        {
            return MessageSender;
        }

        public void SendMessageUpdateViewModel(Control module)
        {
            if(MessageSender != null)
            {
                Console.WriteLine("ControlMessageEmitter SendMessageUpdateViewModel");

                var message = new MessageUpdateViewModel(module.GetModel());
                MessageSender.SendMessage(message);
            }
        }
    }
}
