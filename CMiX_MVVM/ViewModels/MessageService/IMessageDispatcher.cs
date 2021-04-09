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

        IMessageProcessor GetMessageProcessor(Guid id);

        void ProcessMessage(IMessage message);
    }
}