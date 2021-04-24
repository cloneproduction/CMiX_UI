using CMiX.MVVM.ViewModels.MessageService.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.MVVM.ViewModels.MessageService.ModuleMessenger
{
    public class ModuleMessageFactory
    {
        public ModuleMessageFactory(ModuleMessageSender moduleMessageSender)
        {
            ModuleMessageSender = moduleMessageSender;
        }

        private ModuleMessageSender ModuleMessageSender { get; set; }

        public void SendMessageUpdateViewModel(Module module)
        {
            if(ModuleMessageSender != null)
            {
                Console.WriteLine("ModuleMessageSender SendMessage ModuleAddress" + module.ID);
                ModuleMessageSender.SendMessage(new MessageUpdateViewModel(module.ID, module.GetModel()));
            }
                
        }
    }
}
