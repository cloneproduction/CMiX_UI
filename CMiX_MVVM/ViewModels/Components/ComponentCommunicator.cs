using CMiX.MVVM.MessageService;
using CMiX.MVVM.ViewModels.Components.Messages;

namespace CMiX.MVVM.ViewModels.Components
{
    public class ComponentCommunicator : ICommunicator
    {
        public ComponentCommunicator(Component component)
        {
            Component = component;
        }


        private Component Component { get; set; }
        public IMessageReceiver MessageReceiver { get; set; }
        public IMessageSender MessageSender { get; set; }


        private void SetReceiver(IMessageReceiver messageReceiver)
        {
            var messageProcessor = new ComponentMessageProcessor(Component);
            MessageReceiver = new MessageReceiver(messageProcessor);
            messageReceiver?.RegisterReceiver(MessageReceiver);
        }

        private void SetSender(IMessageSender messageSender)
        {
            MessageSender = new MessageSender(Component);
            MessageSender.SetSender(messageSender);
        }

        public void SetNextCommunicator(ICommunicator communicator)
        {
            this.SetReceiver(communicator.MessageReceiver);
            this.SetSender(communicator.MessageSender);
        }


        public void SendMessageAddComponent(Component component)
        {
            MessageSender?.SendMessage(new MessageAddComponent(component));
        }

        public void SendMessage<T>(T obj)
        {
            throw new System.NotImplementedException();
        }
    }
}
