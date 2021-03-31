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

        public MessageDispatcher(Guid componentID)
        {
            ComponentID = componentID;
            MessageProcessors = new Dictionary<Guid, IMessageProcessor>();
        }

        public Guid ComponentID { get; set; }
        public Dictionary<Guid, IMessageProcessor> MessageProcessors { get; set; }


        public event Action<IMessage> MessageNotification;

        private void Colleague_MessageNotification(IViewModelMessage message)
        {
            var handler = MessageNotification;
            if (handler != null)
            {
                handler(message);
            }
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
                //Console.WriteLine("MessageDispatcher NotifyIn " + message.ID);
                //message.Process(col);
            }
        }


        public void RegisterMessageProcessor(IMessageProcessor messageProcessor)
        {
            messageProcessor.MessageNotification += Colleague_MessageNotification;

            Console.WriteLine("RegisterColleague " + messageProcessor.GetType());
            if (MessageProcessors.ContainsKey(messageProcessor.ID))
                MessageProcessors[messageProcessor.ID] = messageProcessor;
            else
                MessageProcessors.Add(messageProcessor.ID, messageProcessor);
        }


        public void UnregisterMessageProcessor(IMessageProcessor messageProcessor)
        {
            messageProcessor.MessageNotification -= Colleague_MessageNotification;
            MessageProcessors.Remove(messageProcessor.ID);
        }


        public void Dispose()
        {
            MessageProcessors.Clear();
        }
    }
}