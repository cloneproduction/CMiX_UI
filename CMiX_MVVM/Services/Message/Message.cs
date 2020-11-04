using NetMQ;
using System;
using CMiX.MVVM.Services;
using Ceras;

namespace CMiX.MVVM.Message
{
    public class Message
    {
        public Message(NetMQMessage netMQMessage)
        {
            NetMQMessage = netMQMessage;
        }

        private NetMQMessage netMQMessage;
        public NetMQMessage NetMQMessage
        {
            get => netMQMessage;
            set => netMQMessage = value;
        }

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
            get => NetMQMessage[2];
        }
    }
}
