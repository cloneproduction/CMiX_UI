using System;

namespace CMiX.MVVM.MessageService
{
    public class MessageSender : IMessageSender
    {
        public MessageSender(Guid id)
        {
            ID = id;
        }

        public Guid ID { get; set; }

        private IMessageSender _messageSender;
        public IMessageSender SetSender(IMessageSender messageSender)
        {
            _messageSender = messageSender;
            return messageSender;
        }

        public void SendMessage(Message message)
        {
            if(_messageSender != null)
            {
                Console.WriteLine("MessageSender SendMessage");
                message.AddID(ID);
                _messageSender?.SendMessage(message);
            }
        }
    }
}