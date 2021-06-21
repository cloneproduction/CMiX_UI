﻿// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using CMiX.Core.Network.Communicators;
using CMiX.Core.Network.Messages;

namespace CMiX.Core.Presentation.ViewModels.Components
{
    public class ComponentCommunicator : Communicator
    {
        public ComponentCommunicator(Component component) : base()
        {
            IIDObject = component;
        }

        public void SendMessageAddComponent(Component component)
        {
            this.SendMessage(new MessageAddComponent(component));
        }

        public void SendMessageRemoveComponent(int index)
        {
            this.SendMessage(new MessageRemoveComponent(index));
        }
    }
}