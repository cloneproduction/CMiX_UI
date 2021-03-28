using CMiX.MVVM.ViewModels.MessageService.Messages;
using System;
using System.Collections.Generic;

namespace CMiX.MVVM.ViewModels.MessageService
{
    public class MessageDispatcher : IDisposable
    {
        public MessageDispatcher(string parentID)
        {
            ParentID = parentID;
            Colleagues = new Dictionary<string, IMessageProcessor>();
        }

        private string ParentID { get; set; }
        private Dictionary<string, IMessageProcessor> Colleagues { get; set; }


        public event Action<IMessageProcessor, IMessage> MessageNotification;

        public void NotifyOut(IViewModelMessage message)
        {
            //SendMessageOut(this, new MessageEventArgs(message));
            //MessageTerminal.SendMessage(message.Address, message);
        }

        public void NotifyIn(IViewModelMessage message)
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