using CMiX.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CMiX.MVVM.Services
{
    public class ServerManager : ViewModel
    {
        public ServerManager()
        {
            Servers = new ObservableCollection<Server>();
            AddServerCommand = new RelayCommand(p => AddServer());
            DeleteServerCommand = new RelayCommand(p => DeleteServer());
        }

        public ICommand AddServerCommand { get; set; }
        public ICommand DeleteServerCommand { get; set; }

        public ObservableCollection<Server> Servers { get; set; }

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

        //public void AddClient()
        //{
        //    Client client = new Client($"Client({ClientID.ToString()})", "127.0.0.1", 1111 + ClientID, $"/Device{ClientID}");
        //    client.Start();
        //    Clients.Add(client);
        //    ClientID++;
        //}

        //public void DeleteClient(object client)
        //{
        //    Client c = client as Client;
        //    c.Stop();
        //    Clients.Remove(c);
        //}
    }
}
