using CMiX.MVVM.MessageService;
using System;

namespace CMiX.MVVM.ViewModels.Components.Messages
{
    public class MessageMoveComponent : Message, IComponentMessage
    {
        public MessageMoveComponent()
        {

        }

        public MessageMoveComponent(Guid id, int oldIndex, int newIndex)
        {
            OldIndex = oldIndex;
            NewIndex = newIndex;
            this.AddID(id);
        }

        public int OldIndex { get; set; }
        public int NewIndex { get; set; }
    }
}
