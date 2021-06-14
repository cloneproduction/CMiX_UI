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


        public void SetCommunicator(ICommunicator communicator)
        {
            MessageReceiver = new MessageReceiver(Component);
            communicator.MessageReceiver?.RegisterReceiver(MessageReceiver);

            MessageSender = new MessageSender(Component);
            MessageSender.SetSender(communicator.MessageSender);
        }

        public void UnsetCommunicator(ICommunicator communicator)
        {
            communicator.MessageReceiver?.UnregisterReceiver(MessageReceiver);
        }


        public void SendMessageAddComponent(Component component)
        {
            MessageSender?.SendMessage(new MessageAddComponent(component));
        }

        public void SendMessageRemoveComponent(int index)
        {
            MessageSender?.SendMessage(new MessageRemoveComponent(index));
        }

        public void SendMessage<T>(T obj)
        {
            throw new System.NotImplementedException();
        }
    }
}