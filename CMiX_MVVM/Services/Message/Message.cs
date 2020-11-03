using NetMQ;
using System;
using CMiX.MVVM.Services;
using Ceras;

namespace CMiX.MVVM.Message
{
    public class Message
    {
        public Message()
        {
            Serializer = new CerasSerializer();
        }

        private NetMQMessage netMQMessage;
        public NetMQMessage NetMQMessage
        {
            get => netMQMessage;
            set
            {
                netMQMessage = value;
                OnMessageUpdated(this);
                Console.WriteLine("NetMQMessage Updated");
            }
        }

        public CerasSerializer Serializer { get; set; }

        public string Topic
        {
            get => NetMQMessage[0].ConvertToString();
        }

        public string MessageAddress
        {
            get => NetMQMessage[1].ConvertToString();
        }

        public object Payload
        {
            get => Serializer.Deserialize<object>(NetMQMessage[2].Buffer);
        }

        public event EventHandler<MessageEventArgs> MessageUpdated;
        private void OnMessageUpdated(Message message)
        {
            if (MessageUpdated != null)
            {
                MessageUpdated(this, new MessageEventArgs(message));
            }
        }
    }
}
