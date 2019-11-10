using Ceras;
using NetMQ;
using NetMQ.Sockets;
using System;

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
                string command = e.Socket.ReceiveFrameString();

                if(command == NetMQActor.EndShimMessage)
                {
                    poller.Stop();
                    return;
                }
                else
                {
                    byte[] byteMessage = e.Socket.ReceiveFrameBytes();
                    publisher.SendMoreFrame(command).SendFrame(byteMessage);
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

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value;}
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

        public void Start()
        {
            if (actor != null)
                return;
            actor = NetMQActor.Create(new ShimHandler(Address));
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
            if (actor == null)
                return;
            Stop();
            Start();
        }

        public void SendObject(string topic, object commandParameter)
        {
            if (actor == null)
                return;

            byte[] Serialized = Serializer.Serialize(commandParameter);
            var message = new NetMQMessage();
            message.Append(topic);
            message.Append(Serialized);
            actor.SendMultipartMessage(message);
            Console.WriteLine("SendObject Topic" + topic);
        }
    }
}