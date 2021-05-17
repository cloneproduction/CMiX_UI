using System;

namespace CMiX.MVVM.MessageService
{
    public interface IMessageSender
    {
        IMessageSender SetSender(IMessageSender messageSender);
        void SendMessage(Message message);
    }
}