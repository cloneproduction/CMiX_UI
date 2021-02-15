using System;
using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Mediator;
using CMiX.MVVM.ViewModels.MessageService.Messages;

namespace CMiX.MVVM.ViewModels
{
    public abstract class Sender : ViewModel, IMessageProcessor, IDisposable
    {
        public Sender(string name, IMessageProcessor parentSender)
        {
            this.Name = name;
            this.parentAddress = parentSender.GetAddress();
            this.MessageDispatcher = parentSender.MessageDispatcher;
            this.MessageDispatcher.RegisterColleague(this);
        }

        private string parentAddress {get; set;}
        private string Name { get; set; }
        public MessageDispatcher MessageDispatcher { get; set; }

        public string GetAddress() => $"{parentAddress}{Name}/";
        public virtual void Dispose() => this.MessageDispatcher?.UnregisterColleague(this.GetAddress());
        public void Send(IMessage message) => this.MessageDispatcher?.Notify(MessageDirection.OUT, message);

        public virtual void Receive(IMessage message) 
        {
            message.Process(this);
        }
    }
}