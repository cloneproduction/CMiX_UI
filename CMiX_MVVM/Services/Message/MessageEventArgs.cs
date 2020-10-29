using CMiX.MVVM.Commands;
using CMiX.MVVM.Message;
using System;

namespace CMiX.MVVM.Services
{
    public class MessageEventArgs : EventArgs
    {
        //public MessageEventArgs(string address, object data, Message.Message message)
        //{
        //    Message = message;
        //    //Address = address;
        //    //Data = data;
        //}

        public MessageEventArgs(Message.Message message)
        {
            Message = message;
        }

        public Message.Message Message { get; set; }

        private string _address;

        public string Address
        {
            get { return Message.MessageAddress; }
        }

        private object _data;
        public object Data
        {
            get { return Message.Payload; }
        }

        //public string Address { get; set; }
        //public object Data { get; set; }
    }
}