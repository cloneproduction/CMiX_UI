using NetMQ;
using System;
using CMiX.MVVM.Services;
using Ceras;

namespace CMiX.MVVM.Message
{
    public class Message
    {
        public Message(string messageAddress, byte[] data)
        {
            MessageAddress = messageAddress;
            Data = data;
        }

        //private NetMQMessage netMQMessage;
        //public NetMQMessage NetMQMessage
        //{
        //    get => netMQMessage;
        //    set => netMQMessage = value;
        //}


        private string _messageAddress;
        public string MessageAddress
        {
            get { return _messageAddress; }
            set { _messageAddress = value; }
        }

        private byte[] _data;
        public byte[] Data
        {
            get { return _data; }
            set { _data = value; }
        }

        //public string Topic
        //{
        //    get => NetMQMessage[0].ConvertToString();
        //}

        //public string MessageAddress
        //{
        //    get => NetMQMessage[1].ConvertToString();
        //}

        //public byte[] Payload
        //{
        //    get => NetMQMessage[2];
        //}
    }
}
