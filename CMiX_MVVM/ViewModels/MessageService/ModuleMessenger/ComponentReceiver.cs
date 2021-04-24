using System;
using System.Collections.Generic;

namespace CMiX.MVVM.ViewModels.MessageService.ModuleMessenger
{
    public class ComponentReceiver : IMessageReceiver
    {
        public ComponentReceiver()
        {
            MessageProcessors = new Dictionary<Guid, IMessageProcessor>();
        }

        Dictionary<Guid, IMessageProcessor> MessageProcessors { get; set; }

        public IMessageProcessor GetMessageProcessor(Guid id)
        {
            if (MessageProcessors.ContainsKey(id))
                return MessageProcessors[id];
            return null;
        }

        public void RegisterReceiver(Guid id, IMessageProcessor component)
        {
            if (!MessageProcessors.ContainsKey(id))
                MessageProcessors.Add(id, component);
        }

        public void UnregisterReceiver(Guid id)
        {
            MessageProcessors.Remove(id);
        }


        public void ReceiveMessage(IMessage message)
        {
            //Console.WriteLine("ComponentReceiver ReceiveMessage ComponentID" + message.ComponentID);
            var messageProcessor = GetMessageProcessor(message.ComponentID);

            if (messageProcessor == null)
                return;

            messageProcessor.ProcessMessage(message);
        }
    }
}