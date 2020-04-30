using System;
using Ceras;
using NetMQ;
using NetMQ.Sockets;

namespace CMiX.MVVM.Message
{
    public class NetMQServer
    {
        public class ShimHandler : IShimHandler
        {
            private PairSocket shim;
            private NetMQPoller poller;
            private PublisherSocket publisher;
            private string Address;

            public ShimHandler(string address)
            {
                Address = address;
            }

            public void Initialise(object state)
            {

            }

            public void Run(PairSocket shim)
            {
                using (publisher = new PublisherSocket())
                {
                    publisher.Bind(Address);
                    publisher.Options.SendHighWatermark = 1000;
                    this.shim = shim;
                    shim.ReceiveReady += OnShimReady;
                    shim.SignalOK();
                    poller = new NetMQPoller { shim, publisher };
                    poller.Run();
                }
            }

            private void OnShimReady(object sender, NetMQSocketEventArgs e)
            {
                NetMQMessage msg = e.Socket.ReceiveMultipartMessage();
                if (msg[0].ConvertToString() == NetMQActor.EndShimMessage)
                {
                    poller.Stop();
                    return;
                }
                else
                {
                    publisher.SendMultipartMessage(msg);
                }
                    
            }

            private void UpdateString(string stringmessage, string propertyToUpdate)
            {
                propertyToUpdate = stringmessage;
            }
        }

        public NetMQServer(string ip, int port)
        {
            IP = ip;
            Port = port;
            Serializer = new CerasSerializer();
        }

        public CerasSerializer Serializer { get; set; }
        private NetMQActor actor;
        public bool IsRunning { get; private set; }


        private int _port;
        public int Port
        {
            get { return _port; }
            set
            {
                _port = value;
                if(IsRunning)
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
                if(IsRunning)
                    ReStart();
            }
        }

        public string Address
        {
            get => String.Format("tcp://{0}:{1}", IP, Port);
        }

        public void Start()
        {
            if (actor != null)
                return;
            actor = NetMQActor.Create(new ShimHandler(Address));
            IsRunning = true;
            Console.WriteLine($"NetMQClient Started with IP {IP}, Port {Port}");
        }

        public void Stop()
        {
            if (actor != null)
            {
                actor.Dispose();
                actor = null;
                IsRunning = false;
            }
        }

        public void ReStart()
        {
            if (actor == null)
                return;
            Stop();
            Start();
        }

        public void SendObject(string topic, string messageAddress, object payload)
        {
            System.Console.WriteLine("NetMQServer Send Model");
            if (actor == null)
                return;

            //byte[] serialParam = Serializer.Serialize(parameter);
            byte[] serialPayload = Serializer.Serialize(payload);
            Console.WriteLine("Data Size = " + serialPayload.Length); 
            //byte[] serialCommand = Serializer.Serialize(command);
            var msg = new NetMQMessage(4);
            msg.Append(topic);
            msg.Append(messageAddress);
            //msg.Append(serialCommand);
            //msg.Append(serialParam);
            msg.Append(serialPayload);
            actor.SendMultipartMessage(msg);
            Console.WriteLine("NetMQServer SendObject with MessageAddress : " + messageAddress + " and Topic : " + topic);
        }
    }
}