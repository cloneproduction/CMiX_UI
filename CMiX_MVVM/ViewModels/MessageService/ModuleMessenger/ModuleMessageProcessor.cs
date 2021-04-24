using CMiX.MVVM.ViewModels.MessageService.Messages;
using System;

namespace CMiX.MVVM.ViewModels.MessageService.ModuleMessenger
{
    public class ModuleMessageProcessor : IMessageProcessor
    {
        public ModuleMessageProcessor(Module module)
        {
            Module = module;
        }

        private Module Module { get; set; }

        public void ProcessMessage(IMessage message)
        {
            //Console.WriteLine("ModuleMessageProcessor ProcessMessage");
            var msg = message as IViewModelMessage;

            if (msg != null)
                Module.SetViewModel(msg.Model);
        }
    }
}
