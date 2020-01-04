using CMiX.MVVM.Commands;
using CMiX.MVVM.Models;
using System;
using System.Collections.ObjectModel;

namespace CMiX.MVVM.Services
{
    public class Receiver
    {
        public Receiver(Client client)
        {
            Client = client;
            Client.MessageReceived += Client_MessageReceived;
        }

        public event EventHandler<MessageEventArgs> MessageReceived;

        private void Client_MessageReceived(object sender, MessageEventArgs e)
        {
            ReceivedCommand = e.Command;
            ReceivedAddress = e.Address;
            ReceivedParameter = e.Parameter;
            ReceivedData = e.Data;

            MessageReceived.Invoke(sender, e);
        }

        public MessageCommand ReceivedCommand { get; set; }
        public string ReceivedAddress { get; set; }
        public object ReceivedData { get; set; }
        public object ReceivedParameter { get; set; }

        public bool Enabled { get; set; }
        public Client Client { get; set; }

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