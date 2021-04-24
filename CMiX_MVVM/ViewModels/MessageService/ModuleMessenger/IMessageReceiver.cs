using CMiX.MVVM.ViewModels.MessageService.ModuleMessenger;
using System;

namespace CMiX.MVVM.ViewModels.MessageService
{
    public interface IMessageReceiver
    {
        void RegisterReceiver(Guid id, IMessageProcessor messageCommunicator);
        void UnregisterReceiver(Guid id);
        void ReceiveMessage(IMessage message);
    }
}
