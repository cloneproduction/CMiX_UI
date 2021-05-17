using System;

namespace CMiX.MVVM.MessageService
{
    public interface IMessageEmitter
    {
        void SetSender(IMessageSender messageSender);
    }
}