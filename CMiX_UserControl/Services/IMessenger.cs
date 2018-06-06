namespace CMiX.Services
{
    public interface IMessenger
    {
        void SendMessage(string address, params object[] args);

        void QueueMessage(string address, params object[] args);

        void SendQueue();
    }
}
