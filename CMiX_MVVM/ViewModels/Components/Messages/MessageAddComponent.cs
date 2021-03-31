using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels.MessageService;
using CMiX.Studio.ViewModels.MessageService;
using System;

namespace CMiX.MVVM.ViewModels.Components.Messages
{
    [Serializable]
    public class MessageAddComponent : IComponentMessage
    {
        public MessageAddComponent()
        {

        }

        public MessageAddComponent(Guid componentID, IComponentModel componentModel)
        {
            ComponentID = componentID;
            ComponentModel = componentModel;
        }

        public Guid ComponentID { get; set; }
        public IComponentModel ComponentModel { get; set; } // must be public because of Ceras...

        public void Process(IMessageDispatcher messageDispatcher)
        {
            IMessageProcessor messageProcessor;

            if (messageDispatcher.MessageProcessors.TryGetValue(ComponentID, out messageProcessor))
            {
                Component component = messageProcessor as Component;

                var newComponent = component.ComponentFactory.CreateComponent(ComponentModel);
                component.Components.Add(newComponent);

                messageDispatcher.RegisterMessageProcessor(newComponent);

                Console.WriteLine("MessageAddComponentProcessed" + "Component Count = " + component.Components.Count);
            }
        }
    }
}
