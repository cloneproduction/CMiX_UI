using CMiX.MVVM.Message;
using System;
using System.ComponentModel;

namespace CMiX.MVVM.Services
{
    public class Client
    {
        public Client()
        {

        }
        public Client(string name, string ip, int port, string topic)
        {
            Enabled = true;
            IsRunning = false;

            Name = name;
            IP = ip;
            Port = port;
            Topic = topic;
            Enabled = true;
            NetMQClient = new NetMQClient(IP, Port, Topic);
            NetMQClient.Message.MessageUpdated += OnNetMQMessageReceived;
        }

        public event EventHandler<MessageEventArgs> MessageReceived;

        private void OnNetMQMessageReceived(object sender, MessageEventArgs e)
        {
            MessageReceived.Invoke(sender, e);
        }

        private NetMQClient NetMQClient { get; set; }

        public bool Enabled { get; set; }
        public string Name { get; set; }
        public string Topic { get; set; }
        public string IP { get; set; }
        public int Port { get; set; }

        public bool IsRunning { get; private set; }

        public void Start()
        {
            NetMQClient.Start();
            IsRunning = true;
        }

        public void Stop()
        {
            NetMQClient.Stop();
            IsRunning = false;
        }
    }
}
