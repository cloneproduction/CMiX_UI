using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Message;
using CMiX.MVVM.Services;
using CMiX.MVVM.Services.Message;
using PubSub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.MVVM.ViewModels.Mediator
{
    public class MessageMediator : IMessageMediator
    {
        public MessageMediator(Component component)
        {
            this.Component = component;
            Colleagues = new Dictionary<string, IColleague>();
        }


        public Dictionary<string, IColleague>  Colleagues { get; set; }
        public Component Component { get; set; }

        public void Notify(string address, IColleague colleague, MessageReceived message)
        {
            if(message.Direction == MessageDirection.IN)
            {
                IColleague col;
                if (Colleagues.TryGetValue(address, out col))
                {
                    if (col != colleague)
                    {
                        colleague.Receive(message);
                    }
                }
            }
            else if(message.Direction == MessageDirection.OUT)
            {
                Component.Hub.Publish<MessageOut>(new MessageOut(address, message.Data));
            }
        }

        public void RegisterColleague(string address, IColleague colleague)
        {
            Console.WriteLine("ColleagueRegistered with address " + address);
            Colleagues.Add(address, colleague);
        }

        public void UnregisterColleague(string address)
        {
            Colleagues.Remove(address);
        }
    }
}
