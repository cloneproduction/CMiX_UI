using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.Studio.ViewModels.MessageService;
using System;
using Ceras;
using CMiX.MVVM.Interfaces;

namespace CMiX.MVVM.ViewModels.MessageService
{
    public class MessengerTerminal : ViewModel
    {
        public MessengerTerminal()
        {
            MessageSender = new MessageSender();
            Serializer = new CerasSerializer();
            Receiver = new Receiver();
            Receiver.MessageReceived += Receiver_MessageReceived;
        }

        public event EventHandler<MessageEventArgs> MessageReceived;
        private void OnMessageReceived(object sender, MessageEventArgs e)
        {
            MessageReceived?.Invoke(sender, e);
        }

        private void Receiver_MessageReceived(object sender, MessageEventArgs e)
        {
            OnMessageReceived(sender, e);
        }

        private CerasSerializer Serializer { get; set; }
        public MessageSender MessageSender { get; set; }
        public Receiver Receiver { get; set; }

        public void StartReceiver(Settings settings)
        {
            Receiver.Start(settings);
        }

        public void SendMessage(string address, Message message)
        {
            MessageSender.SendMessage(address, message.Data);
        }

        public void SendModel(string address, Model model)
        {
            MessageSender.SendMessage(address, Serializer.Serialize(model));
        }

        public void SendComponentUpdate(string address, IComponentModel model)
        {
            MessageSender.SendMessage(address, Serializer.Serialize(model));
        }
    }
}
