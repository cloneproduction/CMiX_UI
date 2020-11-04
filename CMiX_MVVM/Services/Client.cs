using CMiX.MVVM.Message;
using NetMQ;
using NetMQ.Sockets;
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

        //private NetMQClient NetMQClient { get; set; }


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

        //public void Start()
        //{
        //    if (NetMQClient == null)
        //    {
        //        NetMQClient = new NetMQClient(Address, Topic);

        //        NetMQClient.Message.MessageUpdated += OnNetMQMessageReceived;
        //    }

        //    NetMQClient.Start();
        //    IsRunning = true;
        //}

        //public string Address { get; set; }
        public ClientShimHandler ClientShimHandler { get; set; }
        public SubscriberSocket SubscriberSocket { get; set; }
        public NetMQPoller Poller { get; set; }
        public Message.Message Message { get; set; }

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
            NetMQMessage msg = e.Socket.ReceiveMultipartMessage();

            if (msg.FrameCount == 3)
            {
                //Message.Message Message = new Message.Message(msg);
                OnNetMQMessageReceived(this, new MessageEventArgs(new Message.Message(msg)));
                Console.WriteLine(msg[1]);
            }
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