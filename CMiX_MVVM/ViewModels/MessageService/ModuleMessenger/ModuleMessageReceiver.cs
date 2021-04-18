using CMiX.MVVM.ViewModels.MessageService.ModuleMessenger;
using System;
using System.Collections.Generic;

namespace CMiX.MVVM.ViewModels.MessageService
{
    public class ModuleMessageReceiver : IMessageDispatcher
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


        public void RegisterMessageReceiver(MessageCommunicator messageProcessor)
        {
            if (MessageProcessors.ContainsKey(messageProcessor.ID))
                MessageProcessors[messageProcessor.ID] = messageProcessor;
            else
                MessageProcessors.Add(messageProcessor.ID, messageProcessor);
        }

        public void UnregisterMessageReceiver(MessageCommunicator messageProcessor)
        {
            MessageProcessors.Remove(messageProcessor.ID);
        }

        public void ProcessMessage(IMessage message)
        {

        }
    }
}