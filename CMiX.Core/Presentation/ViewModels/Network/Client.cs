// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.Text;
using NetMQ;
using NetMQ.Sockets;
using WatsonTcp;

namespace CMiX.Core.Services
{
    public class Client
    {
        public Client()
        {

        }

        public event EventHandler<DataEventArgs> DataReceived;
        private void OnDataReceived(object sender, DataEventArgs e)
        {
            DataReceived?.Invoke(sender, e);
        }

        public string Topic { get; set; }
        public string IP { get; set; }
        public int Port { get; set; }
        public bool IsRunning { get; private set; }

        public string Address
        {
            get { return String.Format("tcp://{0}:{1}", IP, Port); }
        }

        public SubscriberSocket SubscriberSocket { get; set; }
        public NetMQPoller Poller { get; set; }

        public void Start()
        {
            WatsonTcpClient client = new WatsonTcpClient(IP, 2222);
            client.Events.ServerConnected += ServerConnected;
            client.Events.ServerDisconnected += ServerDisconnected;
            client.Events.MessageReceived += MessageReceived;
            client.Callbacks.SyncRequestReceived = SyncRequestReceived;
            client.Connect();

            Console.WriteLine($"WatsonTcp Started with Address " + Address);

            //SubscriberSocket = new SubscriberSocket(Address);
            //SubscriberSocket.Subscribe(Topic);
            //SubscriberSocket.ReceiveReady += ClientSub_ReceiveReady;

            //Poller = new NetMQPoller();
            //Poller.Add(SubscriberSocket);
            //Poller.RunAsync();
            //Console.WriteLine($"NetMQClient Started with Address " + Address);
        }

        private SyncResponse SyncRequestReceived(SyncRequest arg)
        {
            throw new NotImplementedException();
            //return new SyncResponse("Hello back at you!");
        }

        private void MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            //Console.WriteLine("Message from " + e.IpPort + ": " + Encoding.UTF8.GetString(e.Data));
            OnDataReceived(sender, new DataEventArgs(e.Data));
        }

        private void ServerDisconnected(object sender, DisconnectionEventArgs e)
        {
            Console.WriteLine("Server " + e.IpPort + " disconnected");
        }

        private void ServerConnected(object sender, ConnectionEventArgs e)
        {
            Console.WriteLine("Server " + e.IpPort + " connected");
        }




        private void ClientSub_ReceiveReady(object sender, NetMQSocketEventArgs e)
        {
            string topic = e.Socket.ReceiveFrameString();
            byte[] data = e.Socket.ReceiveFrameBytes();
            OnDataReceived(sender, new DataEventArgs(data));
        }

        public void Stop()
        {
            if (Poller != null)
                Poller.StopAsync();
        }
    }
}
