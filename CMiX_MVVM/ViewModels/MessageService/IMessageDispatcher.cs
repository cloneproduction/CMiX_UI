using System;
using System.Collections.Generic;

namespace CMiX.MVVM.ViewModels.MessageService
{
    public interface IMessageDispatcher
    {
        void DispatchMessage(IMessage message);
        void RegisterMessageProcessor(IMessageProcessor messageProcessor);
        void UnregisterMessageProcessor(IMessageProcessor messageProcessor);
        Dictionary<Guid, IMessageProcessor> MessageProcessors { get; set; }
        event Action<IMessage> MessageNotification;

        void NotifyMessage(IMessage message);
        bool HasHandler();
    }
}