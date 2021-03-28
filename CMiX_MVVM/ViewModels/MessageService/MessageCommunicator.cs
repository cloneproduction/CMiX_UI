using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels.MessageService;
using System;

namespace CMiX.MVVM.ViewModels
{
    public abstract class MessageCommunicator : ViewModel, IMessageProcessor//, IDisposable
    {
        public MessageCommunicator(IMessageProcessor parentSender)
        {
            this.Name = this.GetType().Name;
            this.parentAddress = parentSender.GetAddress();
            this.MessageDispatcher = parentSender.MessageDispatcher;
            this.MessageDispatcher.RegisterColleague(this);
        }

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
        public abstract void SetViewModel(IModel model);
        public abstract IModel GetModel();
    }
}