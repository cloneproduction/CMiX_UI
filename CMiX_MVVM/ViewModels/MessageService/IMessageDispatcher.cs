using CMiX.MVVM.ViewModels.MessageService.MessageSendCOR;
using System;
using System.Collections.Generic;

namespace CMiX.MVVM.ViewModels.MessageService
{
    public interface IMessageDispatcher //: IMessageSendHandler
    {
        //void RegisterMessageReceiver(IMessageReceiveHandler messageProcessor);
        //void UnregisterMessageReceiver(IMessageReceiveHandler messageProcessor);

        //IMessageReceiveHandler GetMessageProcessor(Guid id);

        //void ReceiveMessage(IMessage message);
    }

    public interface IMessageDispatcherReceiver : IMessageReceiveHandler, IMessageDispatcher
    {
        void RegisterMessageReceiver(IMessageReceiveHandler messageProcessor);
        void UnregisterMessageReceiver(IMessageReceiveHandler messageProcessor);

        IMessageReceiveHandler GetMessageProcessor(Guid id);
    }

    public interface IMessageDispatcherSender : IMessageSendHandler, IMessageDispatcher
    {

    }
}