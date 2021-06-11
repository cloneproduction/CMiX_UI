using CMiX.MVVM.ViewModels;
using System;

namespace CMiX.MVVM.MessageService
{
    public class MessageSender : IMessageSender
    {
        public MessageSender(IIDObject idObject)
        {
            _id = idObject.ID;
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