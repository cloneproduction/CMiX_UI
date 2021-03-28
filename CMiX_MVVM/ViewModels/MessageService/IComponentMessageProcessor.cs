using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Services;
using System;

namespace CMiX.MVVM.ViewModels.MessageService
{
    public interface IComponentMessageProcessor : IMessageProcessor
    {
        event Action<IMessageProcessor, IMessage> MessageNotification;
    }
}