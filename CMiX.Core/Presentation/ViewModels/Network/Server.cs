// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using CMiX.Core.Services;
using System;
using System.Text;
using WatsonTcp;

namespace CMiX.Core.Presentation.ViewModels
{
    public class Server : ViewModel
    {
        public Server()
        {
            Enabled = true;
            IsRunning = false;




        }



        private void MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            Console.WriteLine("Message from " + e.IpPort + ": " + Encoding.UTF8.GetString(e.Data));
        }

        private void ClientDisconnected(object sender, DisconnectionEventArgs e)
        {
            Console.WriteLine("Client disconnected: " + this.IP + ": " + e.Reason.ToString());
        }

        private void ClientConnected(object sender, ConnectionEventArgs e)
        {
            ipPort = e.IpPort;
            Console.WriteLine("Client connected: " + e.IpPort);
        }

        private SyncResponse SyncRequestReceived(SyncRequest arg)
        {
            throw new NotImplementedException();
            // return new SyncResponse("Hello back at you!");
        }

        public string ipPort { get; set; }


        private bool _isRunning;
        public bool IsRunning
        {
            get => _isRunning;
            set
            {
                SetAndNotify(ref _isRunning, value);
                Notify(nameof(Status));
            }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetAndNotify(ref _name, value);
        }

        public string Status
        {
            get
            {
                if (IsRunning)
                    return "Running...";
                else
                    return "Stopped";
            }
        }

        private string _ip;
        public string IP
        {
            get => _ip;
            set => SetAndNotify(ref _ip, value);
        }

        private string _topic;
        public string Topic
        {
            get => _topic;
            set => SetAndNotify(ref _topic, value);
        }

        private int _port;
        public int Port
        {
            get => _port;
            set => SetAndNotify(ref _port, value);
        }

        private bool _isRenaming;
        public bool IsRenaming
        {
            get => _isRenaming;
            set => SetAndNotify(ref _isRenaming, value);
        }

        public string Address
        {
            get { return String.Format("tcp://{0}:{1}", IP, Port); }
        }

        public NetMQServer NetMQServer { get; set; }
        public WatsonTcpServer WatsonTcpServer { get; set; }


        public void Send(byte[] data)
        {
            if(Enabled && WatsonTcpServer != null)
            {
                WatsonTcpServer.Send(this.ipPort, data);
                Console.WriteLine("WatsonTcpServer SendObject with  Topic : " + this.Topic + " Data Size = " + data.Length + $"{IP}:{Port}");
            }

            //if (Enabled && NetMQServer != null)
            //{
            //    NetMQServer.SendObject(Topic, data);
            //    Console.WriteLine("NetMQServer SendObject with  Topic : " + this.Topic + " Data Size = " + data.Length);
            //}
        }

        public void Start()
        {
            WatsonTcpServer = new WatsonTcpServer(IP, Port);
            WatsonTcpServer.Events.ClientConnected += ClientConnected;
            WatsonTcpServer.Events.ClientDisconnected += ClientDisconnected;
            WatsonTcpServer.Events.MessageReceived += MessageReceived;
            WatsonTcpServer.Callbacks.SyncRequestReceived = SyncRequestReceived;
            WatsonTcpServer.Start();

            //if (NetMQServer == null)
            //    NetMQServer = new NetMQServer(Address);

            //NetMQServer.Start();
            IsRunning = true;
        }

        public void Stop()
        {
            if(NetMQServer != null)
                NetMQServer.Stop();
            IsRunning = false;
        }

        public void Restart()
        {
            Stop();
            Start();
        }
    }
}
