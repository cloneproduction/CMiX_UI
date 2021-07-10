﻿// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using CMiX.Core.Network.Messages;

namespace CMiX.Core.Presentation.ViewModels.Network
{
    public interface IMessageSender
    {
        void SendMessage(Message message);
    }
}
