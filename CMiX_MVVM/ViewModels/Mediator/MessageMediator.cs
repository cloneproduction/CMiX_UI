using CMiX.MVVM.Services;
using PubSub;
using System;
using System.Collections.Generic;

namespace CMiX.MVVM.ViewModels.Mediator
{
    public class MessageMediator : IMessageMediator
    {
        public MessageMediator()
        {
            Colleagues = new Dictionary<string, IColleague>();
            Hub = Hub.Default;
        }

        private Hub Hub { get; set; }
        private Dictionary<string, IColleague>  Colleagues { get; set; }

        public void Notify(string address, IColleague colleague, Message message)
        {
            if(message.Direction == MessageDirection.IN)
            {
                IColleague col;
                if (Colleagues.TryGetValue(address, out col))
                {
                    if (col != colleague)
                    {
                        col.Receive(message);
                    }
                }
            }
            else if(message.Direction == MessageDirection.OUT)
            {
                Hub.Publish<Message>(message);
            }
        }

        public void RegisterColleague(IColleague colleague)
        {
            Colleagues.Add(colleague.Address, colleague);
        }

        public void UnregisterColleague(string address)
        {
            Colleagues.Remove(address);
        }
    }
}
