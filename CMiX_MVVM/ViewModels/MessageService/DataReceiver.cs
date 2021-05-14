using Ceras;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.ViewModels.Messages;
using CMiX.MVVM.ViewModels.MessageService;
using System;

namespace CMiX.Studio.ViewModels.MessageService
{
    public class DataReceiver : ViewModel, IMessageTerminal
    {
        public DataReceiver()
        {
            Client = new Client();
            Client.DataReceived += Client_DataReceived;
            Serializer = new CerasSerializer();
        }


        public void RegisterReceiver(IMessageReceiver messageReceiver)
        {
            MessageReceiver = messageReceiver;
        }


        private void Client_DataReceived(object sender, DataEventArgs e)
        {
            if (MessageReceiver != null)
            {
                Console.WriteLine("Client_DataReceived Message");

                IMessagePack messageAggregator = Serializer.Deserialize<MessagePack>(e.Data);
                MessageIterator messageIterator = messageAggregator.CreateIterator() as MessageIterator;
                MessageReceiver.ReceiveMessage(messageIterator);
            }
        }


        public string Address
        {
            get { return String.Format("tcp://{0}:{1}", IP, Port); }
        }


        private IMessageReceiver MessageReceiver;
        private CerasSerializer Serializer { get; set; }


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

        private string _name;
        public string Name
        {
            get => _name;
            set => SetAndNotify(ref _name, value);
        }

        private Client _client;
        public Client Client
        {
            get => _client;
            set => SetAndNotify(ref _client, value);
        }

        public Settings GetSettings()
        {
            return new Settings(Name, Client.Topic, Client.IP, Client.Port);
        }

        public void SetSettings(Settings settings)
        {
            if (Client.IsRunning)
                Client.Stop();

            Name = settings.Name;
            Client.Topic = settings.Topic;
            Client.IP = settings.IP;
            Client.Port = settings.Port;
        }

        public void Start(Settings settings)
        {
            if (Client.IsRunning)
                return;

            Name = settings.Name;
            Client.Topic = settings.Topic;
            Client.IP = settings.IP;
            Client.Port = settings.Port;
            Client.Start();
        }

        public void Stop()
        {
            Client.Stop();
        }
    }
}