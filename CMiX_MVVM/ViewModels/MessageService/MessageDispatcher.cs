using CMiX.MVVM.ViewModels.MessageService;
using System;
using System.Collections.Generic;

namespace CMiX.MVVM.ViewModels.Mediator
{
    public class MessageDispatcher : IDisposable
    {

        public MessageDispatcher(MessageTerminal messageTerminal)
        {
            MessageTerminal = messageTerminal;
            Colleagues = new Dictionary<string, IMessageProcessor>();
        }

        private MessageTerminal MessageTerminal { get; set; }
        private Dictionary<string, IMessageProcessor> Colleagues { get; set; }


        public void NotifyOut(IMessage message)
        {
            MessageTerminal.SendMessage(message.Address, message);
        }

        public void NotifyIn(IMessage message)
        {
            IMessageProcessor col;
            if (Colleagues.TryGetValue(message.Address, out col))
                message.Process(col);
        }

        public void RegisterColleague(IMessageProcessor colleague)
        {
            Console.WriteLine("RegisterColleague " + colleague.GetAddress());
            if (Colleagues.ContainsKey(colleague.GetAddress()))
                Colleagues[colleague.GetAddress()] = colleague;
            else
                Colleagues.Add(colleague.GetAddress(), colleague);
        }

        public void UnregisterColleague(string address)
        {
            Colleagues.Remove(address);
        }

        public void Dispose()
        {
            Colleagues.Clear();
        }
    }
}