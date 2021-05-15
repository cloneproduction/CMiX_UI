using CMiX.MVVM.ViewModels;
using System;

namespace CMiX.MVVM.MessageService
{
    public class ModuleMessageFactory
    {
        public ModuleMessageFactory(ModuleSender moduleMessageSender)
        {
            ModuleSender = moduleMessageSender;
        }

        private ModuleSender ModuleSender { get; set; }

        public void SendMessageUpdateViewModel(Module module)
        {
            MessagePack messagePack = new MessagePack();
            messagePack.Messages.Add(new MessageUpdateViewModel(module.ID, module.GetModel()));

            if(ModuleSender != null)
            {
                Console.WriteLine("ModuleSender SendMessageUpdateViewModel ModuleAddress" + module.ID);
                ModuleSender.SendMessagePack(messagePack);
            }
        }
    }
}