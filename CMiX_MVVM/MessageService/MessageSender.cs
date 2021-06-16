using System;

namespace CMiX.MVVM.MessageService
{
    public class MessageSender : IMessageSender
    {
        public MessageSender(Guid id)
        {
            _id = id;
        }


        private Guid _id { get; set; }

        private IMessageSender _messageSender;

        public IMessageSender SetSender(IMessageSender messageSender)
        {
            _messageSender = messageSender;
            return messageSender;
        }

        public void SendMessage(Message message)
        {
            if (_messageSender != null)
            {
                Console.WriteLine("MessageSender SendMessage with ID " + _id);
                message.AddID(_id);
                _messageSender?.SendMessage(message);
            }
        }
    }
}