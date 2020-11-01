using Ceras;
using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels;
using PubSub;
using System.Windows.Input;

namespace CMiX.Studio.ViewModels.MessageService
{
    public class Messenger : Sendable
    {
        public Messenger(int id)
        {
            Hub = Hub.Default;
            Serializer = new CerasSerializer();
            Server = new Server();

            Name = $"Messenger ({id})";

            StartServerCommand = new RelayCommand(p => StartServer());
            StopServerCommand = new RelayCommand(p => StopServer());
            RestartServerCommand = new RelayCommand(p => RestartServer());

            Hub.Subscribe<Message>(this, message =>
            {
                System.Console.WriteLine("MessengerReceivedMessage from  " + message.Address);

                this.Server.Send(message.Address, message.Data);
                // highly interesting things
            });
        }

        public Hub Hub { get; set; }


        public void StartServer()
        {
            Server.Start();
        }

        public void StopServer()
        {
            Server.Stop();
        }

        public void RestartServer()
        {
            Server.Restart();
        }

        public ICommand StartServerCommand { get; }
        public ICommand StopServerCommand { get; }
        public ICommand RestartServerCommand { get; }

        public CerasSerializer Serializer { get; set; }

        public Settings GetSettings()
        {
            return new Settings(Name, Server.Topic, Server.IP, Server.Port);
        }

        public void SetSettings(Settings settings)
        {
            if (Server.IsRunning)
                Server.Stop();

            Name = settings.Name;
            Server.Topic = settings.Topic;
            Server.IP = settings.IP;
            Server.Port = settings.Port;

            Server.Start();
        }


        private string _name;
        public string Name
        {
            get => _name;
            set => SetAndNotify(ref _name, value);
        }

        private Server _server;
        public Server Server
        {
            get => _server;
            set => SetAndNotify(ref _server, value);
        }

        //private Component _selectedComponent;
        //public Component SelectedComponent
        //{
        //    get => _selectedComponent;
        //    set => SetAndNotify(ref _selectedComponent, value);
        //}

        public void Value_SendChangeEvent(object sender, ModelEventArgs e)
        {
            Server.Send(e.MessageAddress, Serializer.Serialize(e.Model));
        }
    }
}