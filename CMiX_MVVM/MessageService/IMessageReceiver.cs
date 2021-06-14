using System;

namespace CMiX.MVVM.MessageService
{
    public interface IMessageReceiver
    {
        Guid GetID();


        void SetReceiver(IMessageReceiver messageReceiver);
        void UnsetReceiver(IMessageReceiver messageReceiver);


        void RegisterReceiver(IMessageReceiver messageReceiver);
        void UnregisterReceiver(IMessageReceiver messageReceiver);
        void ReceiveMessage(IIDIterator idIterator);
    }
}