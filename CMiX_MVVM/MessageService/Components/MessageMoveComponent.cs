using CMiX.MVVM.MessageService;
using System;

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

        public int OldIndex { get; set; }
        public int NewIndex { get; set; }
        public Guid ComponentID { get; set; }
    }
}
