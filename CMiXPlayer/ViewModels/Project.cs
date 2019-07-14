﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Ceras;
using CMiX.MVVM.ViewModels;
using Memento;

namespace CMiXPlayer.ViewModels
{
    public class Project : ViewModel
    {
        #region CONSTRUCTORS
        public Project()
        {
            Clients = new ObservableCollection<Client>();
            Serializer = new CerasSerializer();
            CompoSelector = new FileSelector(string.Empty, "Single", new List<string>() { ".COMPMIX" }, new ObservableCollection<OSCValidation>(), new Mementor());

            AddClientCommand = new RelayCommand(p => AddClient());
            DeleteClientCommand = new RelayCommand(p => DeleteClient(p));
            SendAllCommand = new RelayCommand(p => SendAllClient());
            ResetAllClientCommand = new RelayCommand(p => ResetAllClient());
        }
        #endregion

        #region PROPERTIES
        public ICommand AddClientCommand { get; }
        public ICommand DeleteClientCommand { get; }
        public ICommand SendAllCommand { get; }
        public ICommand ResetAllClientCommand { get; }

        public CerasSerializer Serializer { get; set; }
        public ObservableCollection<Client> Clients { get; set; }
        public FileSelector CompoSelector { get; set; }
        #endregion

        #region METHODS
        private void AddClient()
        {
            var client = new Client(Serializer) { Name = "pouet" };
            Clients.Add(client);
        }

        private void DeleteClient(object client)
        {
            Clients.Remove(client as Client);
        }

        private void SendAllClient()
        {
            foreach (var client in Clients)
            {
                client.SendComposition();
            }
        }

        private void ResetAllClient()
        {
            foreach(var client in Clients)
            {
                client.ResetClient();
            }
        }
        #endregion
    }
}
