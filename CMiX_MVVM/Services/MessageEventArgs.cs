using CMiX.MVVM.Commands;
using System;

namespace CMiX.MVVM.Services
{
    public class MessageEventArgs : EventArgs
    {
        public MessageEventArgs(string address, MessageCommand messageCommand, object parameter, object data)
        {
            Address = address;
            Command = messageCommand;
            Parameter = parameter;
            Data = data;
        }

        public MessageCommand Command { get; set; }
        public string Address { get; set; }
        public object Data { get; set; }
        public object Parameter { get; set; }
    }
}