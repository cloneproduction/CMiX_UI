using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Services;
using CMiX.MVVM.Services.Message;
using CMiX.MVVM.ViewModels;
using PubSub;
using System;

namespace CMiX.Studio.ViewModels.MessageService
{
    public class Receiver : ViewModel
    {
        public Receiver()
        {
            Hub = Hub.Default;
            Client = new Client();
            Client.MessageReceived += Client_MessageReceived;
        }
        public Hub Hub { get; set; }

        //public event EventHandler<ModelEventArgs> DataReceivedEvent;
        //public void OnDataReceivedChange(IModel model, string messageAddress, string parentMessageAddress)
        //{
        //    DataReceivedEvent?.Invoke(this, new ModelEventArgs(model, messageAddress, parentMessageAddress));
        //}

        private void Client_MessageReceived(object sender, MessageEventArgs e)
        { 

            Console.WriteLine("Client_MessageReceived and published to " + e.Address);
            string address = e.Address;
            byte[] data = e.Data;

            Hub.Publish(new MessageReceived(address, data));

            //OnDataReceivedChange(e.Data as IModel, e.Address, String.Empty);
        }

        public string Address
        {
            get { return String.Format("tcp://{0}:{1}", IP, Port); }
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

        public void StartClient()
        {
            Client.Start();
        }

        public void StopClient()
        {
            Client.Stop();
        }
    }
}