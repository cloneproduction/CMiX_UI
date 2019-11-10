using System;
using NetMQ;
using NetMQ.Sockets;

namespace CMiX.MVVM.Message
{
    public class NetMQClient
    {
        public class ShimHandler : IShimHandler
        {
            private PairSocket shim;
            private NetMQPoller poller;
            private SubscriberSocket subscriber;
            private ByteMessage ByteMessage;
            private string Address;
            private string Topic;

            public ShimHandler(ByteMessage byteMessage, string address, string topic)
            {
                this.ByteMessage = byteMessage;
                this.Address = address;
                this.Topic = topic;
            }

            public void Initialise(object state)
            {
            }

            public void Run(PairSocket shim)
            {
                using (subscriber = new SubscriberSocket())
                {
                    subscriber.Connect(Address);
                    subscriber.Subscribe(Topic);
                    subscriber.Options.ReceiveHighWatermark = 1000;
                    subscriber.ReceiveReady += OnSubscriberReady;
                    this.shim = shim;
                    shim.ReceiveReady += OnShimReady;
                    shim.SignalOK();
                    poller = new NetMQPoller { shim, subscriber };
                    poller.Run();
                }
            }

            private void OnSubscriberReady(object sender, NetMQSocketEventArgs e)
            {
                ByteMessage.NetMQMessage = e.Socket.ReceiveMultipartMessage();
            }

            private void OnShimReady(object sender, NetMQSocketEventArgs e)
            {
                string command = e.Socket.ReceiveFrameString();
                if (command == NetMQActor.EndShimMessage)
                {
                    poller.Stop();
                }
            }
        }

        public ByteMessage ByteMessage { get; set; }
        private NetMQActor actor;

        private string _topic;
        public string Topic
        {
            get { return _topic; }
            set
            {
                _topic = value;
                ReStart();
            }
        }

        private int _port;
        public int Port
        {
            get { return _port; }
            set
            {
                _port = value;
                ReStart();
            }
        }

        private string _ip;
        public string IP
        {
            get { return _ip; }
            set
            {
                _ip = value;
                ReStart();
            }
        }

        public string Address
        {
            get { return String.Format("tcp://{0}:{1}", IP, Port); }
        }

        public NetMQClient(string ip, int port, string topic)
        {
            ByteMessage = new ByteMessage();
            IP = ip;
            Port = port;
            Topic = topic;
        }

        public void Start()
        {
            if (actor != null)
                return;
           
            actor = NetMQActor.Create(new ShimHandler(ByteMessage, Address, Topic));
        }

        public void Stop()
        {
            if (actor != null)
            {
                actor.Dispose();
                actor = null;
            }
        }

        public void ReStart()
        {
            if (actor != null)
            {
                Stop();
                Start();
            }
        }
    }
}