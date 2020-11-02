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

        public Client(string topic)
        {
            Enabled = true;
            IsRunning = false;
            Topic = topic;
            Enabled = true;
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

        public string Address
        {
            get { return String.Format("tcp://{0}:{1}", IP, Port); }
        }

        public void Start()
        {
            if (NetMQClient == null)
            {
                NetMQClient = new NetMQClient(Address, Topic);

                //NetMQClient.Message.MessageUpdated += OnNetMQMessageReceived;
            }

            NetMQClient.Start();
            IsRunning = true;
        }

        public void Stop()
        {
            if(NetMQClient != null)
            {
                NetMQClient.Stop();
                //NetMQClient.Message.MessageUpdated -= OnNetMQMessageReceived;
                IsRunning = false;
            }
        }
    }
}
