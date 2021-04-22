using System;

namespace CMiX.MVVM.ViewModels.MessageService
{
    public interface IMessageReceiver<T>
    {
        void RegisterReceiver(T messageCommunicator, Guid id);
        void UnregisterReceiver(Guid id);
        void ReceiveMessage(IMessage message);
    }
}
