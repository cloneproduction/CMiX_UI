using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CMiX.MVVM.Message
{
    public static class Server
    {
        public static void Start()
        {
            new Thread(AcceptClients).Start();
        }

        static void AcceptClients()
        {
            //TcpListener server = null;
            //try
            //{
            //    Int32 port = 58000;
            //    IPAddress localAddr = IPAddress.Parse("127.0.0.1");

            //    // TcpListener server = new TcpListener(port);
            //    server = new TcpListener(localAddr, port);

            //    // Start listening for client requests.
            //    server.Start();

            //    // DO ALL YOUR WORK
            //}
            //catch (SocketException e)
            //{
            //    Console.WriteLine(e.ToString());
            //}
            //finally
            //{
            //    // Stop listening for new clients.
            //    server.Stop();
            //}
            var listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 5555);
            listener.Start();

            while (true)
            {
                var tcpClient = listener.AcceptTcpClient();
                var serverClientHandler = new Messenger(tcpClient);
            }
        }
    }
}
