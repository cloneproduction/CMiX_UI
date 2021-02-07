using System;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Mediator;

namespace CMiX.MVVM.ViewModels
{
    public abstract class Sender : ViewModel, IColleague, IDisposable
    {
        public Sender(string name, IColleague parentSender)
        {
            this.Name = name;
            this.parentAddress = parentSender.GetAddress();
            this.MessageMediator = parentSender.MessageMediator;
            this.MessageMediator.RegisterColleague(this);
        }

        private string parentAddress {get; set;}
        private string Name { get; set; }
        public MessageMediator MessageMediator { get; set; }

        public string GetAddress() => $"{parentAddress}{Name}/";
        public virtual void Dispose() => this.MessageMediator?.UnregisterColleague(this.GetAddress());
        public void Send(Message message) => this.MessageMediator?.Notify(MessageDirection.OUT, message);
        public abstract void Receive(Message message);
    }
}