// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.



using CMiX.Core.Network.Messages;
using MediatR;

namespace CMiX.Core.Presentation.Mediator
{
    public class ReceiveMessageNotification : INotification
    {
        public ReceiveMessageNotification(Message message)
        {
            Message = message;
        }

        public Message Message { get; }
    }
}
