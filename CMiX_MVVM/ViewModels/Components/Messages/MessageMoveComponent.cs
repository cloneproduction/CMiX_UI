using CMiX.MVVM.ViewModels.MessageService;
using CMiX.Studio.ViewModels.MessageService;
using System;
using System.Collections.Generic;

namespace CMiX.MVVM.ViewModels.Components.Messages
{
    public class MessageMoveComponent : IComponentMessage
    {
        public MessageMoveComponent()
        {

        }
        public MessageMoveComponent(Guid componentID, int oldIndex, int newIndex)
        {
            ComponentID = componentID;
            OldIndex = oldIndex;
            NewIndex = newIndex;
        }

        public List<Guid> IDs { get; set; }
        public int OldIndex { get; set; }
        public int NewIndex { get; set; }
        public Guid ComponentID { get; set; }
    }
}
