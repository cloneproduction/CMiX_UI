using System;

namespace CMiX.MVVM.MessageService
{
    public class ModuleSender : IMessageSender
    {
        public ModuleSender(Guid id)
        {
            ID = id;
        }

        Guid ID { get; set; }

        private IMessageSender MessageSender;
        public IMessageSender SetSender(IMessageSender messageSender)
        {
            MessageSender = messageSender;
            return messageSender;
        }

        public void SendMessagePack(IMessagePack messageAggregator)
        {
            if(MessageSender != null)
            {
                messageAggregator.AddMessage(new MessageEmpty(ID));
                MessageSender.SendMessagePack(messageAggregator);
                Console.WriteLine("ModuleSender SendAggregator ComponentAddress");
            }
        }
    }
}