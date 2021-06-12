using CMiX.MVVM.ViewModels;

namespace CMiX.MVVM.MessageService.ViewModels
{
    public class ControlCommunicator : ICommunicator
    {
        public ControlCommunicator(Control control)
        {
            Control = control;
        }

        private Control Control { get; set; }
        public IMessageReceiver MessageReceiver { get; set; }
        public IMessageSender MessageSender { get; set; }


        public void SetCommunicator(ICommunicator communicator)
        {
            var messageProcessor = new ControlMessageProcessor(Control);
            MessageReceiver = new MessageReceiver(messageProcessor);
            communicator.MessageReceiver?.RegisterReceiver(MessageReceiver);

            MessageSender = new MessageSender(Control);
            MessageSender.SetSender(communicator.MessageSender);
        }

        public void UnsetCommunicator(ICommunicator communicator)
        {
            communicator.MessageReceiver?.UnregisterReceiver(MessageReceiver);
        }

        public void SendMessage<T>(T obj)
        {
            var control = obj as Control;
            MessageSender?.SendMessage(new MessageUpdateViewModel(control));
        }
    }
}
