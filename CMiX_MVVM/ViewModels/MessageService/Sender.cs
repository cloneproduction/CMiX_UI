using System;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Mediator;

namespace CMiX.MVVM.ViewModels
{
    public abstract class Sender : ViewModel, IColleague, IDisposable
    {
        public Sender(string name, IColleague parentSender)
        {
            this.Address = $"{parentSender.Address}{name}/";
            this.MessageMediator = parentSender.MessageMediator;
            this.MessageMediator.RegisterColleague(this);
        }

        public string Address { get; set; }
        public MessageMediator MessageMediator { get; set; }

        public void Dispose()
        {
            this.MessageMediator.UnregisterColleague(this.Address);
        }

        public abstract void Receive(Message message);

        public void Send(Message message)
        {
            MessageMediator?.Notify(MessageDirection.OUT, message);
        }
    }
}