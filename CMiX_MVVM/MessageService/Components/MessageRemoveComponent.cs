using CMiX.MVVM.MessageService;
using System;
using System.Collections.Generic;

namespace CMiX.MVVM.ViewModels.Components.Messages
{
    public class MessageRemoveComponent : Message, IComponentMessage
    {
        public MessageRemoveComponent()
        {

        }
        public MessageRemoveComponent(Guid id, int index)
        {
            this.AddID(id);
            Index = index;
        }

        public int Index { get; set; }
    }
}
