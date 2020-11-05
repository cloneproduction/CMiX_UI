using CMiX.MVVM.Message;
using NetMQ;
using NetMQ.Sockets;
using PubSub;
using System;
using System.ComponentModel;


namespace CMiX.MVVM.Services
{
    public class Client
    {
        public Client()
        {
            
        }

        public event EventHandler<MessageEventArgs> MessageReceived;

        private void OnNetMQMessageReceived(object sender, MessageEventArgs e)
        {
            MessageReceived.Invoke(sender, e);
        }

        public bool Enabled { get; set; }
        public string Name { get; set; }
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
            //NetMQMessage msg = e.Socket.ReceiveMultipartMessage();
            string topic = e.Socket.ReceiveFrameString();
            string messageAddress = e.Socket.ReceiveFrameString();
            byte[] data = e.Socket.ReceiveFrameBytes();
            
            OnNetMQMessageReceived(this, new MessageEventArgs(messageAddress, data));
            Console.WriteLine(messageAddress);
        }


        public void Stop()
        {
            if (Poller != null)
                Poller.StopAsync();


            //if(NetMQClient != null)
            //{
            //    NetMQClient.Stop();
            //    NetMQClient.Message.MessageUpdated -= OnNetMQMessageReceived;
            //    IsRunning = false;
            //}
        }
    }
}