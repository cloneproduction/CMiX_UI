﻿using CMiX.MVVM.Message;
using CMiX.MVVM.Commands;

namespace CMiX.MVVM.ViewModels
{
    public class Server : ViewModel
    {
        public Server(string name, string ip, int port, string topic)
        {
            Name = name;
            IP = ip;
            Port = port;
            Topic = topic;
            Enabled = true;
            NetMQServer = new NetMQServer(IP, Port);
        }

        #region PROPERTIES

        private string _name;
        public string Name
        {
            get => _name; 
            set => SetAndNotify(ref _name, value);
        }

        private string _ip;
        public string IP
        {
            get => _ip; 
            set => SetAndNotify(ref _ip, value);
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

        public NetMQServer NetMQServer { get; set; }
        public string Topic { get; set; }
        #endregion


        public void Send(string messageAddress, MessageCommand command, object parameter, object payload)
        {
            if(Enabled)
                NetMQServer.SendObject(Topic, messageAddress, command, parameter, payload);
        }

        public void Start()
        {
            NetMQServer.Start();
        }

        public void Stop()
        {
            NetMQServer.Stop();
        }
    }
}