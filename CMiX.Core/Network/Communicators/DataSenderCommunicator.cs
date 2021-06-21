﻿// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using CMiX.Core.Network.Messages;
using CMiX.Core.Presentation.ViewModels;

namespace CMiX.Core.Network.Communicators
{
    public class DataSenderCommunicator : Communicator
    {
        public DataSenderCommunicator(DataSender dataSender) : base()
        {
            _dataSender = dataSender;
        }

        private DataSender _dataSender { get; set; }

        public override void SendMessage(Message message)
        {
            _dataSender?.SendMessage(message);
        }
    }
}