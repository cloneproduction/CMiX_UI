using System;

namespace CMiX.MVVM.MessageService
{
    public interface IMessageReceiver
    {
        void RegisterMessageReceiver(Guid id, IMessageReceiver messageReceiver);
        void UnregisterMessageReceiver(Guid id);
        void ReceiveMessage(IIDIterator idIterator);
    }
}