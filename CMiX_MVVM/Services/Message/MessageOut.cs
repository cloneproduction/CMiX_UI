using NetMQ;
using System;
using CMiX.MVVM.Services;
using Ceras;

namespace CMiX.MVVM.Message
{
    public class MessageOut
    {
        public MessageOut(string messageAddress, byte[] data)
        {
            MessageAddress = messageAddress;
            Data = data;
        }

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
    }
}
