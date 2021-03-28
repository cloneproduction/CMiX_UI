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


        public event Action<IViewModelMessage> MessageNotification;

        private void Colleague_MessageNotification(IViewModelMessage message)
        {
            var handler = MessageNotification;
            if (handler != null)
            {
                handler(message);
            }
        }


        public void NotifyIn(IViewModelMessage message)
        {
            IMessageProcessor col;
            if (Colleagues.TryGetValue(message.ID, out col))
            {
                //Console.WriteLine("MessageDispatcher NotifyIn " + message.ID);
                message.Process(col);
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