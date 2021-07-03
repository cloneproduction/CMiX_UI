﻿// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

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
            ClientIsConnected = false;
            Status = "Disconnected";
        }


        private string _name;
        public string Name
        {
            get => _name;
            set => SetAndNotify(ref _name, value);
        }


        private string _status;
        public string Status
        {
            get => _status;
            set => SetAndNotify(ref _status, value);
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


        private bool _clientIsConnected;
        public bool ClientIsConnected
        {
            get => _clientIsConnected;
            set => SetAndNotify(ref _clientIsConnected, value);
        }


        private string ipPort { get; set; }
        public WatsonTcpServer WatsonTcpServer { get; set; }


        private void MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            Console.WriteLine("Message from " + e.IpPort + ": " + Encoding.UTF8.GetString(e.Data));
        }

        private void ClientDisconnected(object sender, DisconnectionEventArgs e)
        {
            Console.WriteLine("Client disconnected: " + this.IP + ": " + e.Reason.ToString());
            Status = "Disconnected";
        }

        private void ClientConnected(object sender, ConnectionEventArgs e)
        {
            ipPort = e.IpPort;
            Console.WriteLine("Client connected: " + e.IpPort);
            Status = "Connected";
        }

        private SyncResponse SyncRequestReceived(SyncRequest arg)
        {
            return new SyncResponse(arg, "Hello back at you from Server!");
        }


        public void SendRequestProjectSync()
        {
            try
            {
                SyncResponse resp = WatsonTcpServer.SendAndWait(5000, this.ipPort, "Project model requested from Server");
                Console.WriteLine("Client replied : " + Encoding.UTF8.GetString(resp.Data));
            }
            catch (TimeoutException)
            {
                Console.WriteLine("Too slow...");
            }
        }

        public void Send(byte[] data)
        {
            if (Enabled && WatsonTcpServer != null)
            {
                WatsonTcpServer.Send(this.ipPort, data);
                Console.WriteLine("WatsonTcpServer SendObject with  Topic : " + this.Topic + " Data Size = " + data.Length + $"{IP}:{Port}");
                var pouet = WatsonTcpServer.ListClients();
            }
        }

        public void Start()
        {
            ipPort = $"{IP}:{Port}";
            WatsonTcpServer = new WatsonTcpServer(IP, Port);
            WatsonTcpServer.Events.ClientConnected += ClientConnected;
            WatsonTcpServer.Events.ClientDisconnected += ClientDisconnected;
            WatsonTcpServer.Events.MessageReceived += MessageReceived;
            WatsonTcpServer.Callbacks.SyncRequestReceived = SyncRequestReceived;
            WatsonTcpServer.Start();
        }

        public void Stop()
        {
            if (WatsonTcpServer != null)
            {

                WatsonTcpServer.Stop();
                WatsonTcpServer.DisconnectClients();
                WatsonTcpServer.Events.ClientConnected -= ClientConnected;
                WatsonTcpServer.Events.ClientDisconnected -= ClientDisconnected;
                WatsonTcpServer.Events.MessageReceived -= MessageReceived;
            }
        }

        public void Restart()
        {
            Stop();
            Start();
        }



        //public NetMQServer NetMQServer { get; set; }

        //public string Address
        //{
        //    get { return String.Format("tcp://{0}:{1}", IP, Port); }
        //}

        //public void Start()
        //{
        //    //if (NetMQServer == null)
        //    //    NetMQServer = new NetMQServer(Address);

        //    //NetMQServer.Start();
        //    //IsRunning = true;
        //}

        //public void Send(byte[] data)
        //{
        //    //if (Enabled && NetMQServer != null),
        //    //{
        //    //    NetMQServer.SendObject(Topic, data);
        //    //    Console.WriteLine("NetMQServer SendObject with  Topic : " + this.Topic + " Data Size = " + data.Length);
        //    //}
        //}

        //public void Stop()
        //{
        //    //if (NetMQServer != null)
        //    //    NetMQServer.Stop();
        //    //IsRunning = false;
        //}

        //private bool _isRunning;
        //public bool IsRunning
        //{
        //    get => _isRunning;
        //    set
        //    {
        //        SetAndNotify(ref _isRunning, value);
        //        Notify(nameof(Status));
        //    }
        //}
        //public string Status
        //{
        //    get
        //    {
        //        if (IsRunning)
        //            return "Running...";
        //        else
        //            return "Stopped";
        //    }
    }
}
