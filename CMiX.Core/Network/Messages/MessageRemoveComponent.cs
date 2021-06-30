// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

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
            component.RemoveComponentAtIndex(Index);
            Console.WriteLine("ReceiveMessageRemoveComponent Count is " + component.Components.Count);
        }
    }
}
