using CMiX.MVVM.MessageService;
using System;

namespace CMiX.MVVM.ViewModels
{
    public interface IIDobject
    {
        Guid ID { get; set; }
        IMessageProcessor MessageProcessor { get; set; }
        IMessageReceiver MessageReceiver { get; set; }
    }
}
