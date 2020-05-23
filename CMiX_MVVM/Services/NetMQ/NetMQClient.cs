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
            private Message Message;
            private string Address;
            private string Topic;

            public ShimHandler(Message byteMessage, string address, string topic)
            {
                this.Message = byteMessage;
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
                Message.NetMQMessage = e.Socket.ReceiveMultipartMessage();
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

        public Message Message { get; set; }
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

        public string Address { get; set; }

        public NetMQClient(string address, string topic)
        {
            this.Address = address;
            Message = new Message();
            Topic = topic;
        }

        public void Start()
        {
            if (actor != null)
                return;
           
            actor = NetMQActor.Create(new ShimHandler(Message, Address, Topic));
            Console.WriteLine($"NetMQClient Started with Address " + Address);
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