using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.Studio.ViewModels.MessageService;
using System;
using Ceras;
using CMiX.MVVM.Interfaces;

namespace CMiX.MVVM.ViewModels.MessageService
{
    public class MessengerTerminal
    {
        public MessengerTerminal()
        {
            Sender = new Sender();
            Receiver = new Receiver();
            Receiver.MessageReceived += Receiver_MessageReceived;
            Serializer = new CerasSerializer();
        }

        private void Receiver_MessageReceived(object sender, MessageEventArgs e)
        {
            OnMessageReceived(sender, e);
        }

        private CerasSerializer Serializer { get; set; }
        private Sender Sender { get; set; }
        private Receiver Receiver { get; set; }

        public void StartReceiver(Settings settings)
        {
            Receiver.Start(settings);
        }

        public void SendModel(string address, Model model)
        {
            Sender.SendMessage(address, Serializer.Serialize(model));
        }

        public void SendComponentUpdate(string address, IComponentModel model)
        {
            Sender.SendMessage(address, Serializer.Serialize(model));
        }


        public event EventHandler<MessageEventArgs> MessageReceived;
        private void OnMessageReceived(object sender, MessageEventArgs e)
        {
            MessageReceived.Invoke(sender, e);
        }
    }
}
