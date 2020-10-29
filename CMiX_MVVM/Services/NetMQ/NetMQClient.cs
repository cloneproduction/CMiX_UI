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

            const int MegaByte = 1024;
            public void Run(PairSocket shim)
            {
                using (var queue = new NetMQQueue<int>())
                using (subscriber = new SubscriberSocket())
                {
                    subscriber.Connect(Address);
                    subscriber.Subscribe(Topic);
                    subscriber.Options.ReceiveHighWatermark = 1000;
                    subscriber.Options.MulticastRate = 1000 * MegaByte;
                    subscriber.ReceiveReady += OnSubscriberReady;
                    this.shim = shim;
                    shim.ReceiveReady += OnShimReady;
                    shim.SignalOK();
                    //poller = new NetMQPoller { shim, subscriber };
                    queue.ReceiveReady += Queue_ReceiveReady;
                    poller = new NetMQPoller { queue };
                    poller.RunAsync();

                    while (true)
                    {

                        NetMQMessage message = new NetMQMessage();
                        bool success = subscriber.TryReceiveMultipartMessage(ref message);

                        if (success == false)
                        {
                            continue;
                        }

                        //if (success)
                        Console.WriteLine(message[2].ToString());
                        Message.NetMQMessage = message;
                        Console.WriteLine(Message.MessageAddress);
                        //if (mess != null)
                        //{
                        //    shim.SendMultipartMessage(mess);
                        //    //Console.WriteLine(mess[0].ToString());
                        //    //Message.NetMQMessage = mess;
                        //}

                    }
                }
            }

            private void Queue_ReceiveReady(object sender, NetMQQueueEventArgs<int> e)
            {
                Console.WriteLine("Queue_ReceiveReady");
            }

            private void OnSubscriberReady(object sender, NetMQSocketEventArgs e)
            {
                //Console.WriteLine("OnSubscriberReady");
                //Message.NetMQMessage = e.Socket.ReceiveMultipartMessage();
            }

            private void OnShimReady(object sender, NetMQSocketEventArgs e)
            {
                Console.WriteLine("OnShimReady");
                string command = e.Socket.ReceiveFrameString();
                if (command == NetMQActor.EndShimMessage)
                    poller.Stop();
            }
        }

        /////////////////////

        public NetMQClient(string address, string topic)
        {
            this.Address = address;
            Message = new Message();
            Topic = topic;
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