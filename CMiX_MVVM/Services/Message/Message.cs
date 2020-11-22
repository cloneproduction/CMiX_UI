using System;

namespace CMiX.MVVM.Services
{
    [Serializable]
    public class Message
    {
        public Message()
        {

        }

        public Message(MessageCommand messageCommand, string address, object obj)
        {
            this.Command = messageCommand;
            this.Address = address;
            this.Obj = obj;
        }

        public MessageCommand Command { get; set; }
        public string Address { get; set; }
        public Object Obj { get; set; }
    }

    public enum MessageDirection
    {
        IN,
        OUT
    }

    public enum MessageCommand
    {
        ADD_COMPONENT,
        REMOVE_COMPONENT,
        MOVE_COMPONENT,
        UPDATE_VIEWMODEL
    }
}
