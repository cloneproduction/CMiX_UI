using CMiX.MVVM.ViewModels.MessageService.MessageSendCOR;
using System;
using System.Collections.Generic;

namespace CMiX.MVVM.ViewModels.MessageService
{
    public interface IMessageDispatcher : IHandler
    {
        void RegisterMessageProcessor(IMessageProcessor messageProcessor);
        void UnregisterMessageProcessor(IMessageProcessor messageProcessor);

        Dictionary<Guid, IMessageProcessor> MessageProcessors { get; set; }

        event Action<IMessage> MessageOutNotification;
        event Action<IMessage> MessageInNotification;
        void OnMessageOutNotification(IMessage message);

        void OnMessageInNotification(IMessage message);
        IMessageProcessor GetMessageProcessor(Guid id);

        void ProcessMessage(IMessage message);

        void SendMessage(Func<IMessage> CreateMessage);
    }
}