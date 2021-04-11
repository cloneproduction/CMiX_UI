using CMiX.MVVM.ViewModels.MessageService.MessageSendCOR;
using System;
using System.Collections.Generic;

namespace CMiX.MVVM.ViewModels.MessageService
{
    public class ModuleMessageReceiver
    {
        public ModuleMessageReceiver()
        {
            MessageProcessors = new Dictionary<Guid, MessageCommunicator>();
        }

        private Dictionary<Guid, MessageCommunicator> MessageProcessors { get; set; }

        private MessageCommunicator GetMessageProcessor(Guid id)
        {
            if (MessageProcessors.ContainsKey(id))
                return MessageProcessors[id];
            return null;
        }


        public void RegisterMessageProcessor(MessageCommunicator messageProcessor)
        {
            if (MessageProcessors.ContainsKey(messageProcessor.ID))
                MessageProcessors[messageProcessor.ID] = messageProcessor;
            else
                MessageProcessors.Add(messageProcessor.ID, messageProcessor);
        }

        public void UnregisterMessageProcessor(MessageCommunicator messageProcessor)
        {
            MessageProcessors.Remove(messageProcessor.ID);
        }

        public void ReceiveMessage(IMessage message)
        {

        }
    }
}