using CMiX.MVVM.ViewModels.MessageService.Messages;
using System;
using System.Collections.Generic;

namespace CMiX.MVVM.ViewModels.MessageService
{
    public class MessageDispatcher : IDisposable
    {
        public MessageDispatcher(Guid componentID)
        {
            ComponentID = componentID;
            Colleagues = new Dictionary<Guid, IMessageProcessor>();
        }

        public Guid ComponentID { get; set; }
        private Dictionary<Guid, IMessageProcessor> Colleagues { get; set; }


        public event Action<IMessage> MessageNotification;


        public void NotifyIn(IViewModelMessage message)
        {
            IMessageProcessor col;
            if (Colleagues.TryGetValue(message.ID, out col))
            {
                Console.WriteLine("MessageDispatcher NotifyIn " + message.ID);
                message.Process(col);
            }
        }


        private void Colleague_MessageNotification(IMessage arg2)
        {
            var handler = MessageNotification;
            if (handler != null)
            {
                handler(arg2);
                Console.WriteLine("MessageNotification Raised by ");
            }
        }


        public void RegisterColleague(MessageCommunicator colleague)
        {
            colleague.MessageNotification += Colleague_MessageNotification;

            Console.WriteLine("RegisterColleague " + colleague.GetType());
            if (Colleagues.ContainsKey(colleague.ID))
                Colleagues[colleague.ID] = colleague;
            else
                Colleagues.Add(colleague.ID, colleague);
        }


        public void UnregisterColleague(MessageCommunicator colleague)
        {
            colleague.MessageNotification -= Colleague_MessageNotification;
            Colleagues.Remove(colleague.ID);
        }


        public void Dispose()
        {
            Colleagues.Clear();
        }
    }
}