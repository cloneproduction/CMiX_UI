using CMiX.MVVM.ViewModels;
using System;

namespace CMiX.MVVM.MessageService
{
    public class ViewModelMessageFactory
    {
        public ViewModelMessageFactory(ViewModelSender moduleMessageSender)
        {
            ViewModelSender = moduleMessageSender;
        }

        private ViewModelSender ViewModelSender { get; set; }

        public void SendMessageUpdateViewModel(Control module)
        {
            MessagePack messagePack = new MessagePack();
            messagePack.Messages.Add(new MessageUpdateViewModel(module.ID, module.GetModel()));

            if(ViewModelSender != null)
            {
                Console.WriteLine("ModuleSender SendMessageUpdateViewModel ModuleAddress" + module.ID);
                ViewModelSender.SendMessagePack(messagePack);
            }
        }
    }
}