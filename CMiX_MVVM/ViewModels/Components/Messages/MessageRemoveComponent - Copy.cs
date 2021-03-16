﻿using CMiX.MVVM.ViewModels.Mediator;
using CMiX.MVVM.ViewModels.Components;

namespace CMiX.MVVM.ViewModels.MessageService.Messages
{
    public class MessageRemoveComponent : IMessage
    {
        public MessageRemoveComponent()
        {

        }
        public MessageRemoveComponent(string address, int index)
        {
            Address = address;
            Index = index;
        }

        public int Index { get; set; }
        public object Obj { get; set; }
        public string Address { get; set; }

        public void Process(IMessageProcessor messageProcessor)
        {
            Component component = messageProcessor as Component;
            component.Components[Index].Dispose();
            component.Components.RemoveAt(Index);
        }
    }
}