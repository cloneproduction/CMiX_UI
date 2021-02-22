using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels.Mediator;
using System;

namespace CMiX.MVVM.ViewModels
{
    public abstract class MessageCommunicator : ViewModel, IMessageProcessor, IDisposable
    {
        public MessageCommunicator(string name, IMessageProcessor parentSender)
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

        public abstract void SetViewModel(IModel model);
        public abstract IModel GetModel();
    }
}