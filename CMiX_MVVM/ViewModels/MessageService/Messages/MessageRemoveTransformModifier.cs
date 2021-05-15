using System;
using System.Collections.Generic;

namespace CMiX.MVVM.MessageService
{
    public class MessageRemoveTransformModifier : IMessage
    {
        public MessageRemoveTransformModifier(Guid componentID, int index)
        {
            Index = index;
            ComponentID = componentID;
        }

        public List<Guid> IDs { get; set; }
        public int Index { get; set; }
        public Guid ComponentID { get; set; }
    }
}