using System;

namespace CMiX.MVVM.MessageService
{
    public interface IMessageReceiver
    {
        Guid ID { get; set; }
        void RegisterMessageReceiver(IMessageReceiver messageReceiver);
        void UnregisterMessageReceiver(IMessageReceiver messageReceiver);
        void ReceiveMessage(IIDIterator idIterator);
    }
}