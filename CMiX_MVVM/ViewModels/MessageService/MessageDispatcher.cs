using CMiX.MVVM.Services;
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
            MessageTerminal.MessageReceived += MessageTerminal_MessageReceived;
            Colleagues = new Dictionary<string, IMessageProcessor>();
        }

        private MessageTerminal MessageTerminal { get; set; }
        private Dictionary<string, IMessageProcessor> Colleagues { get; set; }


        private void MessageTerminal_MessageReceived(object sender, MessageEventArgs e)
        {
            this.NotifyIn(e.Message);
        }


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
            MessageTerminal.MessageReceived -= MessageTerminal_MessageReceived;
        }
    }
}