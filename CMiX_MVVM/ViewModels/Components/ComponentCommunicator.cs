using CMiX.MVVM.MessageService;
using CMiX.MVVM.ViewModels.Components.Messages;

namespace CMiX.MVVM.ViewModels.Components
{
    public class ComponentCommunicator
    {
        public ComponentCommunicator()
        {

        }

        public IMessageReceiver MessageReceiver { get; set; }
        public IMessageSender MessageSender { get; set; }

        public virtual void SetReceiver(Component component, IMessageReceiver messageReceiver)
        {
            var messageProcessor = new ComponentMessageProcessor(component);
            MessageReceiver = new MessageReceiver(messageProcessor);
            messageReceiver.RegisterReceiver(MessageReceiver);

            component.Visibility.SetReceiver(MessageReceiver);
        }

        public virtual void SetSender(Component component, IMessageSender messageSender)
        {
            MessageSender = new MessageSender(component);
            MessageSender.SetSender(messageSender);

            component.Visibility.SetSender(MessageSender);
        }

        public void SendMessageAddComponent(Component component)
        {
            MessageSender?.SendMessage(new MessageAddComponent(component));
        }
    }
}
