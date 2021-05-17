using CMiX.MVVM.ViewModels;
using System;

namespace CMiX.MVVM.MessageService.Module
{
    public class ControlMessageEmitter : IMessageEmitter
    {
        public ControlMessageEmitter()
        {

        }

        public IMessageSender MessageSender { get; set; }

        public void SetSender(IMessageSender messageSender)
        {
            MessageSender = messageSender;
        }

        public void SendMessageUpdateViewModel(Guid id, Control module)
        {
            if(MessageSender != null)
            {
                Console.WriteLine("ModuleSender SendMessageUpdateViewModel ModuleAddress" + id);

                var message = new MessageUpdateViewModel(module.ID, module.GetModel());
                MessageSender.SendMessage(message);
            }
        }
    }
}
