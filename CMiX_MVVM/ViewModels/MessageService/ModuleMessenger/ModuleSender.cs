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