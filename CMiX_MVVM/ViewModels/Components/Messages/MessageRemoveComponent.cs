using System;

namespace CMiX.MVVM.ViewModels.Components.Messages
{
    public class MessageRemoveComponent : IComponentManagerMessage

    {
        public MessageRemoveComponent()
        {

        }
        public MessageRemoveComponent(Guid componentID, int index)
        {
            ComponentID = componentID;
            Index = index;
        }

        public int Index { get; set; }
        public Guid ComponentID { get; set; }

        public void Process(ComponentManager componentManager)
        {
            Component component = componentManager.MessageDispatcher.GetMessageProcessor(ComponentID) as Component;
            var componentToDelete = component.Components[Index];
            component.RemoveComponent(componentToDelete);
            componentManager.MessageDispatcher.UnregisterMessageProcessor(componentToDelete);
            Console.WriteLine("MessageRemoveComponentProcessed" + "Component Count = " + component.Components.Count);
        }
    }
}
