using System;
using NetMQ;
using NetMQ.Sockets;

namespace CMiX.MVVM.Services
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
            SubscriberSocket = new SubscriberSocket(Address);
            SubscriberSocket.Subscribe(Topic);
            SubscriberSocket.ReceiveReady += ClientSub_ReceiveReady;

            Poller = new NetMQPoller();
            Poller.Add(SubscriberSocket);
            Poller.RunAsync();
            Console.WriteLine($"NetMQClient Started with Address " + Address);
        }

        private void ClientSub_ReceiveReady(object sender, NetMQSocketEventArgs e)
        {
            string topic = e.Socket.ReceiveFrameString();
            string messageAddress = e.Socket.ReceiveFrameString();
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