using CMiX.MVVM.ViewModels;

namespace CMiX.MVVM.MessageService
{
    public class DataSenderCommunicator : Communicator
    {
        public DataSenderCommunicator(DataSender dataSender) : base()
        {
            _dataSender = dataSender;
        }

        private DataSender _dataSender { get; set; }

        public override void SendMessage(Message message)
        {
            _dataSender?.SendMessage(message);
        }
    }
}
