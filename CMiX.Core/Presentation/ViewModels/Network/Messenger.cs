// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System.Windows.Input;

namespace CMiX.Core.Presentation.ViewModels.Network
{
    public class Messenger : ViewModel
    {
        public Messenger(int id)
        {
            Server = new Server();

            Name = $"Messenger ({id})";

            StartServerCommand = new RelayCommand(p => StartServer());
            RequestProjectReSyncCommand = new RelayCommand(p => RequestProjectResync());
            StopServerCommand = new RelayCommand(p => StopServer());
            RestartServerCommand = new RelayCommand(p => RestartServer());
            RenameCommand = new RelayCommand(p => Rename());
        }

        public ICommand RenameCommand { get; }
        public ICommand RequestProjectReSyncCommand { get; }
        public ICommand StartServerCommand { get; }
        public ICommand StopServerCommand { get; }
        public ICommand RestartServerCommand { get; }

        public Server Server { get; set; }


        private string _name;
        public string Name
        {
            get => _name;
            set => SetAndNotify(ref _name, value);
        }

        private bool _isRenaming;
        public bool IsRenaming
        {
            get => _isRenaming;
            set => SetAndNotify(ref _isRenaming, value);
        }

        public void Rename()
        {
            IsRenaming = true;
            System.Console.WriteLine("DOUBLE CLICK !!");
        }

        public void SendData(byte[] data)
        {
            this.Server.Send(data);
        }


        public Settings GetSettings()
        {
            return new Settings(Name, Server.Topic, Server.IP, Server.Port);
        }


        public void StartServer()
        {
            Server.Start();
        }

        public void RequestProjectResync()
        {
            Server.SendRequestProjectSync();
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
            Name = settings.Name;
            Server.Topic = settings.Topic;
            Server.IP = settings.IP;
            Server.Port = settings.Port;

            Server.Start();
        }
    }
}
