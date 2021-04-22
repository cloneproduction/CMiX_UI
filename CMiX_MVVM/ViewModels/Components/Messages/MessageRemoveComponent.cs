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
    }
}
