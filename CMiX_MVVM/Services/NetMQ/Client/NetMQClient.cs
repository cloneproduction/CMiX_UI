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

            ClientShimHandler = new ClientShimHandler(Message, Address, Topic);
            //ClientShimHandler.ReceiveChangeEvent += ClientShimHandler_ReceiveChangeEvent1;

            actor = NetMQActor.Create(ClientShimHandler);
            
            Console.WriteLine($"NetMQClient Started with Address " + Address);

            NetMQMessage msg = new NetMQMessage();

            ExecuteAsync();
            
            //Console.WriteLine("success " + success);
            //while (true)
            //{
            //    bool success = actor.TryReceiveMultipartMessage(ref msg);

            //    if (success == false)
            //        continue;

            //    Console.WriteLine("POUETPOUET");
            //}
            //var mess = actor.ReceiveFrameString();
            //Console.WriteLine("MessageReceived " + mess);


        }

        async Task ExecuteAsync()
        {
            NetMQMessage msg = new NetMQMessage();
            Console.WriteLine("Enter ExecuteAsync");
            while (true)
            {
                await Task.Delay(42); // for testing

                bool success = actor.TryReceiveMultipartMessage(ref msg);

                if (success == false)
                    continue;

                Console.WriteLine("POUETPOUET");
            }
            //Console.WriteLine("Exit ExecuteAsync");
        }

        private void ClientShimHandler_ReceiveChangeEvent1(object sender, NetMQMessageEventArgs e)
        {
            Console.WriteLine("ClientShimHandler_ReceiveChangeEvent1");
            Console.WriteLine(e.NetMQMessage.FrameCount);
            //throw new NotImplementedException();
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