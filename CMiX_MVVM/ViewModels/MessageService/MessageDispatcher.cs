using System.Collections.Generic;
using System;
using CMiX.MVVM.ViewModels.MessageService;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.MessageService.Messages;

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

        private void MessageTerminal_MessageReceived(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            this.Notify(MessageDirection.IN, message);
        }

        private MessageTerminal MessageTerminal { get; set; }
        private Dictionary<string, IMessageProcessor>  Colleagues { get; set; }

        public void Notify(MessageDirection messageDirection, IMessage message)
        {
            if(messageDirection == MessageDirection.IN)
            {
                IMessageProcessor col;
                if (Colleagues.TryGetValue(message.Address, out col))
                {
                    col.Receive(message);
                }
            }
            else if(messageDirection == MessageDirection.OUT)
            {
                MessageTerminal.SendMessage(message.Address, message);
            }
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