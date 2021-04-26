using CMiX.MVVM.ViewModels.Messages;
using CMiX.MVVM.ViewModels.MessageService.Messages;
using System;

namespace CMiX.MVVM.ViewModels.MessageService.ModuleMessenger
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

        public void SendMessageAggregator(IMessageAggregator messageAggregator)
        {
            if(MessageSender != null)
            {
                //var pouet = messageAggregator as MessageAggregator;
                //pouet.Messages.Add(new MessageEmpty(ID));
                MessageSender.SendMessageAggregator(messageAggregator);
                Console.WriteLine("ModuleSender SendAggregator ComponentAddress");
            }
        }

        public void SendMessage(IMessage message)
        {
            if (MessageSender != null)
            {
                Console.WriteLine("ModuleSender SendMessage ComponentAddress" + message.ComponentID);
                message.ComponentID = ID;
                MessageSender.SendMessage(message);
                
            }
        }
    }
}