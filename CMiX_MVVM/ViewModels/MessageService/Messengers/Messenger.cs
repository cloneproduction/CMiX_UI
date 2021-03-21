using CMiX.MVVM.ViewModels;
using System.Windows.Input;

namespace CMiX.Studio.ViewModels.MessageService
{
    public class Messenger : ViewModel
    {
        public Messenger(int id)
        {
            Server = new Server();

            Name = $"Messenger ({id})";

            StartServerCommand = new RelayCommand(p => StartServer());
            StopServerCommand = new RelayCommand(p => StopServer());
            RestartServerCommand = new RelayCommand(p => RestartServer());
        }

        public ICommand StartServerCommand { get; }
        public ICommand StopServerCommand { get; }
        public ICommand RestartServerCommand { get; }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetAndNotify(ref _name, value);
        }

        private Server Server { get; set; }

        public void SendMessage(string address, byte[] data)
        {
            this.Server.Send(address, data);
        }

        public bool ServerIsRunning { get => Server.IsRunning; }

        public Settings GetSettings()
        {
            return new Settings(Name, Server.Topic, Server.IP, Server.Port);
        }

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
    }
}