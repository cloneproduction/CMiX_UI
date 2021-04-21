using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels.MessageService.ModuleMessenger;
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
            ComponentMessageReceiver componentMessageReceiver = componentManager.MessageReceiver;
            var parentComponent = componentMessageReceiver.GetMessageProcessor(ComponentID) as Component;
            var newComponent = parentComponent.ComponentFactory.CreateComponent(ComponentModel);
            newComponent.SetReceiver(componentMessageReceiver);

            parentComponent.AddComponent(newComponent);

            Console.WriteLine("MessageAddComponentProcessed" + "Component Count = " + parentComponent.Components.Count);
        }
    }
}