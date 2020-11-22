using CMiX.MVVM.Services;
using CMiX.Studio.ViewModels.MessageService;
using System;
using Ceras;

namespace CMiX.MVVM.ViewModels.MessageService
{
    public class MessengerTerminal : ViewModel
    {
        public MessengerTerminal()
        {
            MessageSender = new MessageSender();

            var config = new SerializerConfig() { DefaultTargets = TargetMember.AllPublic };
            Serializer = new CerasSerializer(config);

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
        private Receiver Receiver { get; set; }

        public void StartReceiver(Settings settings)
        {
            Receiver.Start(settings);
        }

        public void SendMessage(string address, Message message)
        {
            byte[] data = Serializer.Serialize(message);
            MessageSender.SendMessage(address, data);
        }
    }
}
