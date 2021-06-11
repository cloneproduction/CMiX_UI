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


        private void SetReceiver(IMessageReceiver messageReceiver)
        {
            MessageReceiver = new MessageReceiver(Control.MessageProcessor);
            messageReceiver?.RegisterReceiver(MessageReceiver);
        }

        private void SetSender(IMessageSender messageSender)
        {
            MessageSender = new MessageSender(Control);
            MessageSender.SetSender(messageSender);
        }

        public void SetNextCommunicator(ICommunicator communicator)
        {
            this.SetReceiver(communicator.MessageReceiver);
            this.SetSender(communicator.MessageSender);
        }


        public void SendMessage<T>(T obj)
        {
            var control = obj as Control;
            MessageSender?.SendMessage(new MessageUpdateViewModel(control));
        }
    }
}
