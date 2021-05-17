using System;
using System.Collections.Generic;

namespace CMiX.MVVM.MessageService
{
    public class MessageRemoveTransformModifier : Message
    {
        public MessageRemoveTransformModifier(Guid id, int index)
        {
            Index = index;
            this.AddID(id);
        }

        public int Index { get; set; }
    }
}