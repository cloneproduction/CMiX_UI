using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels.MessageService;

namespace CMiX.MVVM.ViewModels.Components.Messages
{
    public class MessageInsertComponent : IComponentMessage
    {
        public MessageInsertComponent()
        {

        }
        public MessageInsertComponent(string address, IComponentModel componentModel, int index)
        {
            this.Address = address;
            this.ComponentModel = componentModel;
        }

        private int Index { get; set; }
        public IComponentModel ComponentModel { get; set; }
        public object Obj { get; set; }
        public string Address { get; set; }

        public void Process(IMessageProcessor messageProcessor)
        {
            Component component = messageProcessor as Component;
            var newComponent = component.CreateChild();// ComponentFactory.CreateComponent(ComponentModel);
            component.Components.Insert(Index, newComponent);
        }

        public void Process(IMessageProcessor viewModel, IMessageTerminal messageTerminal)
        {
            throw new System.NotImplementedException();
        }
    }
}