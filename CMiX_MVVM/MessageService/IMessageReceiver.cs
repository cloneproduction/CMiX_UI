using System;

namespace CMiX.MVVM.MessageService
{
    public interface IMessageReceiver
    {
        Guid GetID();
        void RegisterReceiver(IMessageReceiver messageReceiver);
        void UnregisterReceiver(IMessageReceiver messageReceiver);
        void ReceiveMessage(IIDIterator idIterator);
    }
}