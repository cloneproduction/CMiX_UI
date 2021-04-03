using CMiX.MVVM.Interfaces;
using System;

namespace CMiX.MVVM.ViewModels.Components.Messages
{
    [Serializable]
    public class MessageAddComponent : IComponentManagerMessage
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

        public void Process(ComponentManager componentManager)
        {
            Component component = componentManager.MessageDispatcher.GetMessageProcessor(ComponentID) as Component;
            var newComponent = component.ComponentFactory.CreateComponent(ComponentModel);
            componentManager.MessageDispatcher.RegisterMessageProcessor(newComponent);
            component.AddComponent(newComponent);
            Console.WriteLine("MessageAddComponentProcessed" + "Component Count = " + component.Components.Count);
        }
    }
}