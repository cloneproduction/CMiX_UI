using CMiX.MVVM.ViewModels;

namespace CMiX.MVVM.MessageService
{
    public class ControlMessageProcessor : IMessageProcessor
    {
        public ControlMessageProcessor(Control module, MessageReceiver messageReceiver)
        {
            Module = module;
            MessageReceiver = messageReceiver;
        }

        private Control Module { get; set; }
        private MessageReceiver MessageReceiver { get; set; }

        public void DispatchIterator(IIDIterator messageIterator)
        {
            //MessageReceiver.ReceiveMessage(messageIterator);
        }

        public void ProcessMessage(Message message)
        {
            System.Console.WriteLine("ModuleMessageProcessor ProcessMessage");
            var msg = message as IViewModelMessage;

            if (msg != null)
                Module.SetViewModel(msg.Model);
        }
    }
}