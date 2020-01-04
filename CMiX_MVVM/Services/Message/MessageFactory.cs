using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.MVVM.Message
{
    public class MessageFactory
    {
        public MessageFactory()
        {

        }

        int ServerID = 0;
        int ClientID = 0;

        public Server CreateServer()
        {
            Server server = new Server($"Server({ServerID.ToString()})", "127.0.0.1", 1111 + ServerID, $"/Device{ServerID}");
            server.Start();
            ServerID++;
            return server;
        }

        public Client CreateClient()
        {
            Client client = new Client($"Client({ClientID.ToString()})", "127.0.0.1", 1111 + ClientID, $"/Device{ClientID}");
            client.Start();
            ClientID++;
            return client;
        }
    }
}
