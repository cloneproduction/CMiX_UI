using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CMiX.MVVM
{
    static class Server
    {
        public static void Start()
        {
            new Thread(AcceptClients).Start();
        }

        static void AcceptClients()
        {
            var listener = new TcpListener(IPAddress.Any, 43210);
            listener.Start();

            while (true)
            {
                var tcpClient = listener.AcceptTcpClient();

                var serverClientHandler = new Messenger(tcpClient);
            }
        }
    }
}
