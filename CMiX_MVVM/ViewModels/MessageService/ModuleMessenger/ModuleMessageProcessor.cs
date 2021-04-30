using CMiX.MVVM.ViewModels.Messages;
using CMiX.MVVM.ViewModels.MessageService.Messages;

namespace CMiX.MVVM.ViewModels.MessageService.ModuleMessenger
{
    public class ModuleMessageProcessor : IMessageProcessor
    {
        public ModuleMessageProcessor(Module module)
        {
            Module = module;
        }

        private Module Module { get; set; }

        public void DispatchIterator(IMessageIterator messageIterator)
        {

        }

        public void ProcessMessage(IMessage message)
        {
            System.Console.WriteLine("ModuleMessageProcessor ProcessMessage");
            var msg = message as IViewModelMessage;

            if (msg != null)
                Module.SetViewModel(msg.Model);
        }
    }
}