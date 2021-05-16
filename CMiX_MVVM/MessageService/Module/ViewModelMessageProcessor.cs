using CMiX.MVVM.ViewModels;

namespace CMiX.MVVM.MessageService
{
    public class ViewModelMessageProcessor : IMessageProcessor
    {
        public ViewModelMessageProcessor(Control module)
        {
            Module = module;
        }

        private Control Module { get; set; }

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