using CMiX.MVVM.ViewModels;
using CMiX.MVVM.ViewModels.MessageService.Messages;
using System;

namespace CMiX.MVVM.Services
{
    [Serializable]
    public class Message : IMessage
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

        public Message(MessageCommand messageCommand, string address, object obj, object commandParameter) : this(messageCommand, address, obj)
        {
            this.CommandParameter = commandParameter;
        }

        public MessageCommand Command { get; set; }
        public string Address { get; set; }
        public object Obj { get; set; }
        public object CommandParameter { get; set; }

        public void Process(ISenderTest viewModel)
        {
            //throw new NotImplementedException();
        }
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
        INSERT_COMPONENT,
        MOVE_COMPONENT,
        ADD_TRANSFORMMODIFIER,
        REMOVE_TRANSFORMMODIFIER,
        UPDATE_VIEWMODEL,
    }
}
