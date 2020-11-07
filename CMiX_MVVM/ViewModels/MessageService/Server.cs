﻿using System;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Message;

namespace CMiX.Studio.ViewModels.MessageService
{
    public class Server : ViewModel
    {
        public Server()
        {
            Enabled = true;
            IsRunning = false;
        }

        #region PROPERTIES
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
        #endregion

        public void Send(string messageAddress, byte[] message)
        {
            if (Enabled && NetMQServer != null)
            {
                NetMQServer.SendObject(Topic, messageAddress, message);
                Console.WriteLine("Data Size = " + message.Length);
                Console.WriteLine("NetMQServer SendObject with MessageAddress : " + messageAddress + " and Topic : " + this.Topic);
            }
        }

        public void Start()
        {
            if(NetMQServer == null)
                NetMQServer = new NetMQServer(Address);

            NetMQServer.Start();
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