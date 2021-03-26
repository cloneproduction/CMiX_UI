using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.MessageService;
using System;
using System.Collections.Generic;

namespace CMiX.MVVM.ViewModels.MessageService
{
    public class MessageDispatcher : IDisposable
    {
        public MessageDispatcher()
        {
            Colleagues = new Dictionary<string, IMessageProcessor>();
        }

        public MessageTerminal MessageTerminal { get; set; }
        private Dictionary<string, IMessageProcessor> Colleagues { get; set; }


        public event EventHandler<MessageEventArgs> SendMessageOut;
        private void OnSendMessageOut(object sender, MessageEventArgs e) => SendMessageOut?.Invoke(sender, e);

        public void NotifyOut(IMessage message)
        {
            //SendMessageOut(this, new MessageEventArgs(message));
            //MessageTerminal.SendMessage(message.Address, message);
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