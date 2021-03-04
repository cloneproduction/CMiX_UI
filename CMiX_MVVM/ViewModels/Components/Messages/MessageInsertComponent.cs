using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels.Mediator;
using CMiX.MVVM.ViewModels.Components;

namespace CMiX.MVVM.ViewModels.MessageService.Messages
{
    public class MessageInsertComponent : IMessage
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
        private IComponentModel ComponentModel { get; set; }
        public object Obj { get; set; }
        public string Address { get; set; }

        public void Process(IMessageProcessor messageProcessor)
        {
            Component component = messageProcessor as Component;
            var newComponent = component.ComponentFactory.CreateComponent(component, ComponentModel);
            component.Components.Insert(Index, newComponent);
        }
    }
}