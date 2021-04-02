using System;
using System.Collections.Generic;

namespace CMiX.MVVM.ViewModels.MessageService
{
    public interface IMessageDispatcher
    {
        void RegisterMessageProcessor(IMessageProcessor messageProcessor);
        void UnregisterMessageProcessor(IMessageProcessor messageProcessor);

        Dictionary<Guid, IMessageProcessor> MessageProcessors { get; set; }

        event Action<IMessage> MessageOutNotification;
        event Action<IMessage> MessageInNotification;
        void OnMessageOutNotification(IMessage message);

        void OnMessageInNotification(IMessage message);
    }
}