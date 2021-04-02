using CMiX.MVVM.Interfaces;
using System;

namespace CMiX.MVVM.ViewModels.MessageService
{
    public interface IMessageProcessor
    {
        Guid ID { get; set; }
        void SetViewModel(IModel model);
        IModel GetModel();

        event Action<IMessage> MessageOutNotification;
        //void ProcessMessage(IMessage message);
    }
}