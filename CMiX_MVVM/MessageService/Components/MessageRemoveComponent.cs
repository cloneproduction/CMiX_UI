using CMiX.MVVM.MessageService;
using System;
using System.Collections.Generic;

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

        public List<Guid> IDs { get; set; }
        public int Index { get; set; }
        public Guid ComponentID { get; set; }
    }
}
