using CMiX.MVVM.Message;
using CMiX.MVVM.Commands;
using System;

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
            IsRunning = false;

            Start();
        }

        #region PROPERTIES
        private string Address
        {
            get { return String.Format("tcp://{0}:{1}", IP, Port); }
        }

        public bool IsRunning { get; set; }

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


        public void Send(string messageAddress, byte[] message)
        {
            if(Enabled)
                NetMQServer.SendObject(Topic, messageAddress, message);
        }

        public void Start()
        {
            NetMQServer = new NetMQServer(Address);
            NetMQServer.Start();
            IsRunning = true;
        }

        public void Stop()
        {
            NetMQServer.Stop();
            IsRunning = false;
        }

        public void Restart()
        {
            Stop();
            Start();
            //NetMQServer.ReStart();
            Console.WriteLine("Restart Server");
        }
    }
}