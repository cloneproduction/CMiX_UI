using CMiX.MVVM.ViewModels.Messages;
using CMiX.MVVM.ViewModels.MessageService.Messages;
using System;

namespace CMiX.MVVM.ViewModels.MessageService.ModuleMessenger
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
            MessageAggregator messageAggregator = new MessageAggregator();
            messageAggregator.AddMessage(new MessageUpdateViewModel(module.ID, module.GetModel()));

            

            if(ModuleSender != null)
            {
                Console.WriteLine("ModuleSender SendMessage ModuleAddress" + module.ID);
                ModuleSender.SendMessage(new MessageUpdateViewModel(module.ID, module.GetModel()));

                ModuleSender.SendMessageAggregator(messageAggregator);
            }
        }
    }
}