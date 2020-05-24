using System;
using System.Collections.ObjectModel;
using CMiX.MVVM.Commands;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels;

namespace CMiX.MVVM.Services
{
    public class Receiver
    {
        public Receiver(ObservableCollection<Client> clients)
        {
            Clients = clients;
            SubscribeToClientsMessageReceived();
        }

        public event EventHandler<MessageEventArgs> MessageReceived;

        private void Client_MessageReceived(object sender, MessageEventArgs e)
        {
            ReceivedAddress = e.Address;
            ReceivedData = e.Data;
            MessageReceived.Invoke(sender, e);
        }

        public void SubscribeToClientsMessageReceived()
        {
            foreach (var client in Clients)
            {
                client.MessageReceived += Client_MessageReceived;
            }
        }

        public void UpdateViewModel<T>(string messageAddress, ICopyPasteModel<T> viewModel)
        {
            if (messageAddress == ReceivedAddress
                && ReceivedData != null
                && MessageCommand.VIEWMODEL_UPDATE == ReceivedCommand)
            {
                viewModel.PasteModel((T)ReceivedData);
            }
        }

        public MessageCommand ReceivedCommand { get; set; }
        public string ReceivedAddress { get; set; }
        public object ReceivedData { get; set; }
        //public object ReceivedParameter { get; set; }

        public bool Enabled { get; set; }
        public ObservableCollection<Client> Clients { get; set; }

        public void Disable()
        {
            this.Enabled = false;
        }

        public void Enable()
        {
            this.Enabled = true;
        }
    }
}