using System;

namespace CMiX.MVVM.MessageService
{
    public interface IMessageProcessor
    {
        void ProcessMessage(Message message);
        Guid GetID();
    }
}