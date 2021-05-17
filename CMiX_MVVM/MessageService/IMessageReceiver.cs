using System;

namespace CMiX.MVVM.MessageService
{
    public interface IMessageReceiver
    {
        void RegisterMessageProcessor(Guid id, IMessageProcessor messageProcessor);
        void UnregisterMessageProcessor(Guid id);
        void ReceiveMessage(IIDIterator idIterator);
    }
}