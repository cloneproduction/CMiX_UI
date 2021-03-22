using Ceras;
using CMiX.MVVM.Services;
using CMiX.Studio.ViewModels.MessageService;
using System;

namespace CMiX.MVVM.ViewModels.MessageService
{
    public class MessageTerminal : ViewModel
    {
        public MessageTerminal()
        {
            MessageSender = new MessageSender();
            MessageReceiver = new MessageReceiver();
            MessageReceiver.MessageReceived += Receiver_MessageReceived;

            var config = new SerializerConfig();// TargetMember.AllPublic | TargetMember.AllPrivate };
            Serializer = new CerasSerializer(config);
        }

        public event EventHandler<MessageEventArgs> MessageReceived;
        private void OnMessageReceived(object sender, MessageEventArgs e)
        {
            MessageReceived?.Invoke(sender, e);
        }

        private void Receiver_MessageReceived(object sender, MessageEventArgs e)
        {
            OnMessageReceived(sender, e);
            //Console.WriteLine("MessageReceived " + e.Message.Address);
        }

        private CerasSerializer Serializer { get; set; }
        public MessageSender MessageSender { get; set; }
        public MessageReceiver MessageReceiver { get; set; }

        public void StartReceiver(Settings settings)
        {
            MessageReceiver.Start(settings);
        }

        public void SendMessage(string address, IMessage message)
        {
            if (MessageSender.HasMessengerRunning)
            {
                byte[] data = Serializer.Serialize(message);
                MessageSender.SendMessage(address, data);
            }
        }
    }
}
