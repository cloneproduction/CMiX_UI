// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using Ceras;
using CMiX.Core.Network.Communicators;
using CMiX.Core.Network.Messages;
using CMiX.Core.Presentation.ViewModels.Network;
using CMiX.Core.Services;

namespace CMiX.Core.Presentation.ViewModels
{
    public class MessageService : ViewModel, IMessageSender
    {
        public MessageService()
        {
            ServerManager = new ServerManager();
            Serializer = new CerasSerializer();
        }


        private CerasSerializer Serializer { get; set; }
        public ServerManager ServerManager { get; set; }
        public Client Client { get; set; }



        public void SetCommunicator(Communicator communicator)
        {
            communicator.MessageSent += Communicator_MessageSent;
        }

        public void UnSetCommunicator(Communicator communicator)
        {
            communicator.MessageSent -= Communicator_MessageSent;
        }


        private void Communicator_MessageSent(object sender, MessageEventArgs e)
        {
            this.SendMessage(e.Message);
        }

        private void SendMessage(Message message)
        {
            var data = Serializer.Serialize(message);
            foreach (var server in ServerManager.Servers)
            {
                server.Send(data);
                Console.WriteLine("DataSender SendMessage");
            }
        }
    }
}
