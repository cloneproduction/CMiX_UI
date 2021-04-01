using CMiX.MVVM.ViewModels.MessageService.Messages;
using System;
using System.Collections.Generic;

namespace CMiX.MVVM.ViewModels.MessageService
{
    public class MessageDispatcher : IDisposable, IMessageDispatcher
    {
        public MessageDispatcher()
        {
            MessageProcessors = new Dictionary<Guid, IMessageProcessor>();
        }

        public Dictionary<Guid, IMessageProcessor> MessageProcessors { get; set; }


        public event Action<IMessage> MessageNotification;
        public void NotifyMessage(IMessage message)
        {
            var handler = MessageNotification;
            if (handler != null)
            {
                handler(message);
            }
        }

        public bool HasHandler()
        {
            return MessageNotification != null;
        }

        public void DispatchMessage(IMessage message)
        {
            message.Process(this);
            //IMessageProcessor col;
            //if (Colleagues.TryGetValue(message.ModuleID, out col))
            //{
            //    //Console.WriteLine("MessageDispatcher NotifyIn " + message.ID);
            //    //message.Process(col);
            //}
        }

        public void NotifyIn(IViewModelMessage message)
        {
            IMessageProcessor col;
            if (MessageProcessors.TryGetValue(message.ModuleID, out col))
            {
                Console.WriteLine("MessageDispatcher NotifyIn " + message.ComponentID);
                //message.Process(col);
            }
        }


        public void RegisterMessageProcessor(IMessageProcessor messageProcessor)
        {
            messageProcessor.MessageNotification += NotifyMessage;

            Console.WriteLine("RegisterColleague " + messageProcessor.GetType());
            if (MessageProcessors.ContainsKey(messageProcessor.ID))
                MessageProcessors[messageProcessor.ID] = messageProcessor;
            else
                MessageProcessors.Add(messageProcessor.ID, messageProcessor);
        }


        public void UnregisterMessageProcessor(IMessageProcessor messageProcessor)
        {
            //messageProcessor.MessageNotification -= NotifyMessage;
            MessageProcessors.Remove(messageProcessor.ID);
        }


        public void Dispose()
        {
            MessageProcessors.Clear();
        }
    }
}