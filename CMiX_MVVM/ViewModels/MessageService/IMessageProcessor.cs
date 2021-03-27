using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Services;
using System;

namespace CMiX.MVVM.ViewModels.MessageService
{
    public interface IMessageProcessor
    {
        MessageDispatcher MessageDispatcher { get; set; }
        string GetAddress();
        void SetViewModel(IModel model);
        IModel GetModel();

        event Action<IMessageProcessor, IMessage> MessageNotification;
    }
}