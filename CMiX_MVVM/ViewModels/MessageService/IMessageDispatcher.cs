using CMiX.MVVM.ViewModels.MessageService.MessageSendCOR;
using System;
using System.Collections.Generic;

namespace CMiX.MVVM.ViewModels.MessageService
{
    public interface IMessageDispatcher : IMessageSendHandler
    {
        void RegisterMessageProcessor(IMessageReceiveHandler messageProcessor);
        void UnregisterMessageProcessor(IMessageReceiveHandler messageProcessor);

        ///Dictionary<Guid, IMessageReceiveHandler> MessageProcessors { get; set; }

        IMessageReceiveHandler GetMessageProcessor(Guid id);

        void ProcessMessage(IMessage message);
    }
}