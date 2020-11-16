using CMiX.MVVM.Services;
using CMiX.Studio.ViewModels.MessageService;
using System;

namespace CMiX.MVVM.ViewModels.MessageService
{
    public class MessengerTerminal
    {
        public MessengerTerminal()
        {
            Sender = new MessengerManager();
            Receiver = new Receiver();
        }

        private MessengerManager Sender { get; set; }
        private Receiver Receiver { get; set; }

        public void StartSender(Settings settings)
        {
            
        }

        public void StartReceiver(Settings settings)
        {
            Receiver.Start(settings);
        }
        public void SendMessage(string address, byte[] data)
        {

        }

        public event EventHandler<MessageEventArgs> MessageReceived;
        private void OnMessageReceived(object sender, MessageEventArgs e)
        {
            MessageReceived.Invoke(sender, e);
        }
    }
}
