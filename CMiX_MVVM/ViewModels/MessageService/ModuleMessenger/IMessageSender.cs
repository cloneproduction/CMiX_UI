using CMiX.MVVM.ViewModels.Messages;

namespace CMiX.MVVM.ViewModels.MessageService
{
    public interface IMessageSender
    {
        //void SendMessage(IMessage message);

        void SendMessageAggregator(IMessageAggregator messageAggregator);
    }
}