using CMiX.MVVM.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            ComponentModel = componentModel;
            Address = address;
        }


        public string Address { get; set; }
        public object Obj { get; set; }
        public IComponentModel ComponentModel { get; set; }  //must be public because of Ceras Serializer

        public void Process(ViewModel viewModel)
        {
            Component component = viewModel as Component;
            var newComponent = component.ComponentFactory.CreateComponent(component, ComponentModel);
            component.Components.Add(newComponent);
            Console.WriteLine("MessageAddComponentProcessed Count is " + component.Components.Count);
        }
    }
}
