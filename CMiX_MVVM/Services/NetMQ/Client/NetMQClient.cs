using System;
using System.Threading.Tasks;
using CMiX.MVVM.Services;
using NetMQ;
using NetMQ.Sockets;

namespace CMiX.MVVM.Message
{
    public class NetMQClient
    {
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
        public ClientShimHandler ClientShimHandler { get; set; }


        public void Start()
        {
            if (actor != null)
                return;

            //ClientShimHandler = new ClientShimHandler(Message, Address, Topic);
            //ClientShimHandler.ReceiveChangeEvent += ClientShimHandler_ReceiveChangeEvent1;

            //actor = NetMQActor.Create(ClientShimHandler);
            
            Console.WriteLine($"NetMQClient Started with Address " + Address);


            NetMQPoller poller = new NetMQPoller();
            SubscriberSocket clientSub = new SubscriberSocket(Address);
            clientSub.Subscribe(Topic);
            clientSub.ReceiveReady += ClientSub_ReceiveReady;
            poller.Add(clientSub);
            poller.RunAsync();
            Console.WriteLine("poller.RunAsync()");
        }


        private void ClientSub_ReceiveReady(object sender, NetMQSocketEventArgs e)
        {
            NetMQMessage msg = e.Socket.ReceiveMultipartMessage();

            if(msg.FrameCount == 3)
            {
                Message.NetMQMessage = msg;

                Console.WriteLine("receive ready ");
                Console.WriteLine(Message.MessageAddress);
            }

        }

        //async Task<NetMQMessage> ExecuteAsync()
        //{
        //    NetMQMessage msg = new NetMQMessage();
        //    Console.WriteLine("Enter ExecuteAsync");
        //    while (true)
        //    {
        //        await Task.Delay(10); // for testing

        //        bool success = actor.TryReceiveMultipartMessage(ref msg);

        //        if (success == false)
        //            continue;

        //        Console.WriteLine("POUETPOUET");
                
        //        //return msg;
        //    }
            
        //    //Console.WriteLine("Exit ExecuteAsync");
        //}


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