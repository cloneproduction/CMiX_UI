﻿// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using CMiX.Core.Models;
using CMiX.Core.Presentation.ViewModels.Components;

namespace CMiX.Core.Network.Messages
{
    public class MessageInsertComponent : Message
    {
        public MessageInsertComponent()
        {

        }

        public MessageInsertComponent(IComponentModel componentModel, int index)
        {
            Index = index;
            this.ComponentModel = componentModel;
        }

        public int Index { get; set; }
        public IComponentModel ComponentModel { get; set; }

        public override void Process<T>(T receiver)
        {
            var component = receiver as Component;
        }
    }
}