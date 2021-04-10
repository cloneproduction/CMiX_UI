using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels.MessageService.MessageSendCOR;
using System;

namespace CMiX.MVVM.ViewModels.MessageService
{
    public interface IMessageProcessor
    {
        Guid ID { get; set; }
        void SetViewModel(IModel model);
        IModel GetModel();

        //void ReceiveMessage(IMessage message);
        //void SendMessage(IMessage message);
    }
}