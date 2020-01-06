namespace CMiX.Services
{
    public interface ISender
    {
        void SendMessage(string address, params object[] args);

        void QueueMessage(string address, params object[] args);

        void SendQueue();

        void QueueObject(object obj);
    }
}
