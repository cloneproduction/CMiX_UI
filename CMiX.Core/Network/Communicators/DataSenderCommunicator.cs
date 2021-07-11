// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using CMiX.Core.Network.Messages;
using CMiX.Core.Presentation.ViewModels;

namespace CMiX.Core.Network.Communicators
{
    public class DataSenderCommunicator : Communicator
    {
        public DataSenderCommunicator(MessageSender messageSender) : base()
        {
            MessageSender = messageSender;
        }

        private MessageSender MessageSender { get; set; }

        public override void SendMessage(Message message)
        {
            //MessageSender?.SendMessage(message);
        }
    }
}
