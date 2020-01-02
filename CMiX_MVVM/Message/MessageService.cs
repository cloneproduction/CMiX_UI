﻿using CMiX.MVVM.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CMiX.MVVM.Services
{
    public class MessageService : ViewModel
    {
        public MessageService()
        {
            Servers = new ObservableCollection<Server>();
            AddServer();

            AddServerCommand = new RelayCommand(p => AddServer());
            DeleteServerCommand = new RelayCommand(p => DeleteServer(p));
        }

        public ICommand AddServerCommand { get; set; }
        public ICommand DeleteServerCommand { get; set; }

        public ObservableCollection<Server> Servers { get; set; }
        public ObservableCollection<Messenger> Messengers { get; set; }

        private Server _selectedServer;
        public Server SelectedServer
        {
            get => _selectedServer;
            set => SetAndNotify(ref _selectedServer, value);
        }

        int servernameid = 0;

        public Messenger CreateMessenger()
        {
            return new Messenger(Servers);
        }

        public void AddServer()
        {
            Server server = new Server($"Server({servernameid.ToString()})", "127.0.0.1", 1111 + servernameid, $"/Device{servernameid}");
            server.Start();
            Servers.Add(server);
            servernameid++;
        }

        private void DeleteServer(object server)
        {
            Server serv = server as Server;
            serv.Stop();
            Servers.Remove(serv);
        }
    }
}
