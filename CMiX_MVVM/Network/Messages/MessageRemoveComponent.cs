using CMiX.Core.Presentation.ViewModels.Components;
using System;

namespace CMiX.Core.Network.Messages
{
    public class MessageRemoveComponent : Message
    {
        public MessageRemoveComponent()
        {

        }
        public MessageRemoveComponent(int index)
        {
            Index = index;
        }

        public int Index { get; set; }

        public override void Process<T>(T receiver)
        {
            var component = receiver as Component;
            //Component componentToRemove = component.Components.ElementAt(Index);
            component.RemoveComponentAtIndex(Index);
            Console.WriteLine("ReceiveMessageRemoveComponent Count is " + component.Components.Count);
        }
    }
}