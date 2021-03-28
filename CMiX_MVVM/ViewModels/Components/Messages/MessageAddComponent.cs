using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels.MessageService;
using System;

namespace CMiX.MVVM.ViewModels.Components.Messages
{
    [Serializable]
    public class MessageAddComponent : IComponentMessage
    {
        public MessageAddComponent()
        {

        }

        public MessageAddComponent(Guid id, IComponentModel componentModel)
        {
            ID = id;
            ComponentModel = componentModel;
        }

        public Guid ID { get; set; }
        public IComponentModel ComponentModel { get; set; } // must be public because of Ceras...

        public void Process(IMessageProcessor messageProcessor, IMessageTerminal messageTerminal)
        {
            Component component = messageProcessor as Component;
            var newComponent = component.ComponentFactory.CreateComponent(ComponentModel);
            component.Components.Add(newComponent);
            messageTerminal.RegisterMessageProcessor(newComponent);

            Console.WriteLine("MessageAddComponentProcessed");
            Console.WriteLine("Component Count = " + component.Components.Count);
        }

        public void Process(IMessageProcessor viewModel)
        {
            throw new NotImplementedException();
        }
    }
}
