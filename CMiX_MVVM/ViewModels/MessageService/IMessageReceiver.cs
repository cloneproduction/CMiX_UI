using System;

namespace CMiX.MVVM.MessageService
{
    public interface IMessageReceiver
    {
        void RegisterReceiver(Guid id, IMessageProcessor messageCommunicator);
        void UnregisterReceiver(Guid id);
        void ReceiveMessage(IMessageIterator messageIterator);
    }
}