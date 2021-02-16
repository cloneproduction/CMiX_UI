using CMiX.MVVM.Interfaces;
using System;

namespace CMiX.MVVM.ViewModels.MessageService.Messages
{
    [Serializable]
    public class MessageAddComponent : IMessage
    {
        public MessageAddComponent()
        {

        }

        public MessageAddComponent(string address, IComponentModel componentModel)
        {
            Address = address;
            ComponentModel = componentModel;

        }

        public string Address { get; set; }
        public object Obj { get; set; }
        public IComponentModel ComponentModel { get; set; } // must be public because of Ceras...

        public void Process(ISenderTest viewModel)
        {
            Component component = viewModel as Component;
            var newComponent = component.ComponentFactory.CreateComponent(component, ComponentModel);
            component.Components.Add(newComponent);
        }
    }
}
