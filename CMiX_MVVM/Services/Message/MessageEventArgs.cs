using CMiX.MVVM.Commands;
using System;

namespace CMiX.MVVM.Services
{
    public class MessageEventArgs : EventArgs
    {
        public MessageEventArgs(string address, object data)
        {
            Address = address;
            Data = data;
        }

        public string Address { get; set; }
        public object Data { get; set; }
    }
}