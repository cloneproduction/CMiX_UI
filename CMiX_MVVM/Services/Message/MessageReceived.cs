using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.MVVM.Services.Message
{
    public class MessageReceived
    {
        public MessageReceived(MessageDirection messageDirection, string address, byte[] data)
        {

            this._direction = messageDirection;
            Address = address;
            Data = data;
        }

        private MessageDirection _direction;
        public MessageDirection Direction
        {
            get { return _direction; }
        }

        private string _address;
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        private byte[] _data;
        public byte[] Data
        {
            get { return _data; }
            set { _data = value; }
        }
    }

    public enum MessageDirection
    {
        IN,
        OUT
    }
}
