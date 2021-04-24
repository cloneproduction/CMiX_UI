using CMiX.MVVM.ViewModels.MessageService;
using System;

namespace CMiX.MVVM.ViewModels.Components.Messages
{
    public class MessageRemoveComponent : IComponentMessage
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
