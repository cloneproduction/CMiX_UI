using CMiX.MVVM.Services;
using PubSub;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CMiX.MVVM.ViewModels.Mediator
{
    public class MessageMediator : IMessageMediator
    {
        public MessageMediator()
        {
            Colleagues = new Dictionary<string, IColleague>();
        }

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
                ComponentOwner.MessengerTerminal.SendMessage(address, message);

            }
        }

        private IComponent ComponentOwner { get; set; }

        public void SetComponentOwner(IComponent component)
        {
            ComponentOwner = component;
        }

        public IComponent GetComponentOwner()
        {
            return ComponentOwner;
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
