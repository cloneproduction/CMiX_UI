using System;
using CMiX.MVVM.ViewModels.MessageService;

namespace CMiX.MVVM.Services
{
    public class MessageEventArgs : EventArgs
    {
        public MessageEventArgs(IMessage message)
        {
            Message = message;
        }

        public IMessage Message  { get; set; }
    }
}