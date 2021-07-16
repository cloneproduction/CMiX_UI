// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using CMiX.Core.Presentation.ViewModels.Network;
using WatsonTcp;

namespace CMiX.Core.Presentation.ViewModels
{
    public class Server : ViewModel
    {
        public Server(int id)
        {
            Enabled = true;
            ClientIsConnected = false;
            DataSent = false;

            Status = "Disconnected";
            ConnectedClients = new ObservableCollection<ConnectedClient>();
            Statistics = new ServerStatistics();


            StartCommand = new RelayCommand(p => Start());
            //RequestProjectReSyncCommand = new RelayCommand(p => RequestProjectResync(p as Project));
            StopCommand = new RelayCommand(p => Stop());
            RestartCommand = new RelayCommand(p => Restart());
            RenameCommand = new RelayCommand(p => Rename());
        }

        public ICommand RenameCommand { get; }
        public ICommand RequestProjectReSyncCommand { get; }
        public ICommand StartCommand { get; }
        public ICommand StopCommand { get; }
        public ICommand RestartCommand { get; }



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

        private bool _dataSent;
        public bool DataSent
        {
            get => _dataSent;
            set => SetAndNotify(ref _dataSent, value);
        }


        private ObservableCollection<ConnectedClient> _connectedClients;
        public ObservableCollection<ConnectedClient> ConnectedClients
        {
            get => _connectedClients;
            set => SetAndNotify(ref _connectedClients, value);
        }


        private string ipPort { get; set; }
        public WatsonTcpServer WatsonTcpServer { get; set; }
        public ServerStatistics Statistics { get; set; }


        public Settings GetSettings()
        {
            return new Settings(Name, Topic, IP, Port);
        }

        public void SetSettings(Settings settings)
        {
            Name = settings.Name;
            Topic = settings.Topic;
            IP = settings.IP;
            Port = settings.Port;
            Start();
        }


        public void Rename()
        {
            IsRenaming = true;
            System.Console.WriteLine("DOUBLE CLICK !!");
        }

        private void MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            Console.WriteLine("Message from " + e.IpPort + ": " + Encoding.UTF8.GetString(e.Data));
        }


        private void ClientDisconnected(object sender, DisconnectionEventArgs e)
        {
            Console.WriteLine("Client disconnected: " + this.IP + ": " + e.Reason.ToString());
            Status = "Disconnected";
            ClientIsConnected = false;
        }


        private void ClientConnected(object sender, ConnectionEventArgs e)
        {
            Console.WriteLine("Client connected: " + e.IpPort);
            ipPort = e.IpPort;

            ObservableCollection<ConnectedClient> connectedClients = new ObservableCollection<ConnectedClient>();
            foreach (var item in WatsonTcpServer.ListClients().ToList())
            {
                ConnectedClient connectedClient = new ConnectedClient(item);
                connectedClients.Add(connectedClient);
            }

            ClientIsConnected = true;
            ConnectedClients = connectedClients;
            Status = "Connected";
        }


        private SyncResponse SyncRequestReceived(SyncRequest arg)
        {

            return new SyncResponse(arg, "Hello back at you from Server!");
        }




        public void SendRequestProjectSync(byte[] data)
        {
            if (WatsonTcpServer != null)
            {
                try
                {
                    SyncResponse resp = WatsonTcpServer.SendAndWait(5000, this.ipPort, data);
                    //SyncResponse resp = WatsonTcpServer.SendAndWait(5000, this.ipPort, "Project model requested from Server");
                    Console.WriteLine("Client replied : " + Encoding.UTF8.GetString(resp.Data));
                }
                catch (TimeoutException)
                {
                    Console.WriteLine("Too slow...");
                }
            }
        }


        public void Send(byte[] data)
        {
            if (Enabled && WatsonTcpServer != null)
            {
                WatsonTcpServer.Send(this.ipPort, data);
                Statistics.Update(WatsonTcpServer);
                Console.WriteLine("WatsonTcpServer SendObject with  Topic : " + this.Topic + " Data Size = " + data.Length + "to address : " + $"{IP}:{Port}");
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
                ClientIsConnected = false;
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
