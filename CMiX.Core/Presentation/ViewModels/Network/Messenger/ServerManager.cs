﻿// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System.Collections.ObjectModel;
using System.Windows.Input;
using CMiX.Core.Presentation.Views;
using MvvmDialogs;

namespace CMiX.Core.Presentation.ViewModels.Network
{
    public class ServerManager : ViewModel
    {
        public ServerManager()
        {
            ServerFactory = new ServerFactory();
            DialogService = new DialogService(new CustomFrameworkDialogFactory(), new CustomTypeLocator());
            Servers = new ObservableCollection<Server>();

            AddServerCommand = new RelayCommand(p => AddServer());
            DeleteServerCommand = new RelayCommand(p => DeleteServer(p as Server));
            RenameServerCommand = new RelayCommand(p => RenameServer(p as Server));
            EditMessengerSettingsCommand = new RelayCommand(p => EditMessengerSettings(p as Server));
        }


        public ICommand EditMessengerSettingsCommand { get; }
        public ICommand AddServerCommand { get; set; }
        public ICommand DeleteServerCommand { get; set; }
        public ICommand RenameServerCommand { get; set; }
        private IDialogService DialogService { get; set; }



        private ServerFactory ServerFactory { get; set; }
        public ObservableCollection<Server> Servers { get; set; }


        private Server _selectedServer;
        public Server SelectedServer
        {
            get => _selectedServer;
            set => SetAndNotify(ref _selectedServer, value);
        }


        public void EditMessengerSettings(Server server)
        {
            Settings settings = server.GetSettings();
            bool? success = DialogService.ShowDialog<MessengerSettingsWindow>(this, settings);
            if (success == true)
                server.SetSettings(settings);
        }

        public void AddServer()
        {
            var messenger = ServerFactory.CreateServer();
            Servers.Add(messenger);
        }

        private void DeleteServer(Server server)
        {
            if (server != null)
            {
                server.Stop();
                Servers.Remove(server);

                if (Servers.Count > 0)
                {
                    SelectedServer = Servers[0];
                    return;
                }

                SelectedServer = null;
            }
        }

        private void RenameServer(Server obj)
        {
            if (obj != null)
                obj.IsRenaming = true;
        }
    }
}
