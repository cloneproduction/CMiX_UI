using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels.MessageService.MessageSendCOR;
using System;

namespace CMiX.MVVM.ViewModels.MessageService
{
    public interface IMessageProcessor : IHandler
    {
        Guid ID { get; set; }
        void SetViewModel(IModel model);
        IModel GetModel();

        void ProcessMessage(IMessage message);
        void SendMessage(IMessage message);
    }
}