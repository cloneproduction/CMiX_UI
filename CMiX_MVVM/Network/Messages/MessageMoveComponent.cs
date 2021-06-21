﻿// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using CMiX.Core.Presentation.ViewModels.Components;
using System;

namespace CMiX.Core.Network.Messages 
{ 
    public class MessageMoveComponent : Message
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

        public override void Process<T>(T receiver)
        {
            var component = receiver as Component;
        }
    }
}
