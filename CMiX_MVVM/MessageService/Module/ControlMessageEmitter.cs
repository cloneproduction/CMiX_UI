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

        public void SendMessageUpdateViewModel(Control module)
        {
            Console.WriteLine("ControlMessageEmitter SendMessageUpdateViewModel");
            var message = new MessageUpdateViewModel(module.GetModel());
            MessageSender.SendMessage(message);
        }
    }
}