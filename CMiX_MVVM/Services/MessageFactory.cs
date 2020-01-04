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

        int servernameid = 0;

        public Server CreateServer()
        {
            Server server = new Server($"Server({servernameid.ToString()})", "127.0.0.1", 1111 + servernameid, $"/Device{servernameid}");
            server.Start();
            servernameid++;
            return server;
        }

        //public Client CreateClient()
        //{

        //}
    }
}
