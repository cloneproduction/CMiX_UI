using CMiX.MVVM.ViewModels;
using CMiX.Studio.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CMiX.Studio.Services
{
    public class ServerManager : ViewModel
    {
        public ServerManager(Project project)
        {
            Servers = project.Servers;
            AddServerCommand = new RelayCommand(p => AddServer());
            DeleteServerCommand = new RelayCommand(p => DeleteServer());
            RenameServerCommand = new RelayCommand(p => RenameServer(p));
        }

        public ICommand AddServerCommand { get; set; }
        public ICommand DeleteServerCommand { get; set; }
        public ICommand RenameServerCommand { get; set; }

        public ObservableCollection<Server> Servers { get; set; }

        public Project Project { get; set; }

        private Server _selectedServer;
        public Server SelectedServer
        {
            get => _selectedServer;
            set => SetAndNotify(ref _selectedServer, value);
        }

        int ServerID = 0;
        //int ClientID = 0;

        public void AddServer()
        {
            Server server = new Server($"Server({ServerID.ToString()})", "127.0.0.1", 1111 + ServerID, $"/Device{ServerID}");
            server.Start();
            Servers.Add(server);
            ServerID++;
        }

        private void DeleteServer()
        {
            if(SelectedServer != null)
            {
                SelectedServer.Stop();
                Servers.Remove(SelectedServer);
            }
        }

        private void RenameServer(object server)
        {
            if (server != null)
            {
                Server serv = server as Server;
                serv.IsRenaming = true;
            }
        }
    }
}
