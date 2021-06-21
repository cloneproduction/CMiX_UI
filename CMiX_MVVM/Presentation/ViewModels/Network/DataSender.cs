﻿// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using Ceras;
using CMiX.Core.Network.Messages;
using CMiX.Core.Presentation.Views;
using CMiX.Studio.ViewModels;
using MvvmDialogs;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CMiX.Core.Presentation.ViewModels
{
    public class DataSender : ViewModel
    {
        public DataSender()
        {
            Serializer = new CerasSerializer();

            MessengerFactory = new MessengerFactory();
            Messengers = new ObservableCollection<Messenger>();
            DialogService = new DialogService(new CustomFrameworkDialogFactory(), new CustomTypeLocator());

            AddMessengerCommand = new RelayCommand(p => AddMessenger());
            DeleteMessengerCommand = new RelayCommand(p => DeleteMessenger(p as Messenger));
            RenameMessengerCommand = new RelayCommand(p => RenameServer(p as Server));
            EditMessengerSettingsCommand = new RelayCommand(p => EditMessengerSettings(p as Messenger));
        }


        private MessengerFactory MessengerFactory { get; set; }
        private CerasSerializer Serializer { get; set; }
        private IDialogService DialogService { get; set; }

        public ICommand EditMessengerSettingsCommand { get; }
        public ICommand AddMessengerCommand { get; set; }
        public ICommand DeleteMessengerCommand { get; set; }
        public ICommand RenameMessengerCommand { get; set; }


        private ObservableCollection<Messenger> _messengers;
        public ObservableCollection<Messenger> Messengers
        {
            get => _messengers;
            set => SetAndNotify(ref _messengers, value);
        }

        public void EditMessengerSettings(Messenger messenger)
        {
            Settings settings = messenger.GetSettings();
            bool? success = DialogService.ShowDialog<MessengerSettingsWindow>(this, settings);
            if (success == true)
                messenger.SetSettings(settings);
        }

        public void AddMessenger()
        {
            var messenger = MessengerFactory.CreateMessenger();
            Messengers.Add(messenger);
        }

        private void DeleteMessenger(Messenger messenger)
        {
            if (messenger != null)
            {
                messenger.StopServer();
                Messengers.Remove(messenger);
            }
        }

        private void RenameServer(Server obj)
        {
            if (obj != null)
                obj.IsRenaming = true;
        }


        public void SendMessage(Message message)
        {
            byte[] data = Serializer.Serialize(message);

            foreach (var messenger in Messengers)
            {
                messenger.SendData(data);
                Console.WriteLine("DataSender SendMessageAggregator");
            }
        }
    }
}