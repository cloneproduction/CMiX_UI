using CMiX.MVVM.MessageService;
using CMiX.MVVM.ViewModels.Components.Messages;

namespace CMiX.MVVM.ViewModels.Components
{
    public class ComponentCommunicator : Communicator
    {
        public ComponentCommunicator(Component component) : base()
        {
            IIDObject = component;
        }

        public void SendMessageAddComponent(Component component)
        {
            this.SendMessage(new MessageAddComponent(component));
        }

        public void SendMessageRemoveComponent(int index)
        {
            this.SendMessage(new MessageRemoveComponent(index));
        }
    }
}