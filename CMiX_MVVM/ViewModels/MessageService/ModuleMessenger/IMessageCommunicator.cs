using System;

namespace CMiX.MVVM.ViewModels.MessageService
{
    public interface IMessageCommunicator
    {
        Guid ID { get; set; }

        void SetSender(IMessageSender messageSender);
        //void SetReceiver(IMessageReceiver messageReceiver);


        //void ReceiveMessage(IMessage message);
    }
}
