using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels.MessageService;
using CMiX.MVVM.ViewModels.MessageService.Messages;
using System;

namespace CMiX.MVVM.ViewModels
{
    public abstract class MessageCommunicator : ViewModel, IMessageProcessor//, IDisposable
    {
        public MessageCommunicator(MessageDispatcher messageDispatcher)
        {
            ID = Guid.NewGuid();
            //this.Name = this.GetType().Name;
            //this.parentAddress = parentSender.GetAddress();
            //this.MessageDispatcher = parentSender.MessageDispatcher;
            messageDispatcher.RegisterColleague(this);
        }

        public MessageCommunicator(Guid id, MessageDispatcher messageDispatcher)
        {
            this.ID = id;

            //this.MessageDispatcher = parentSender.MessageDispatcher;
            messageDispatcher.RegisterColleague(this);
        }


        public event Action<IMessage> MessageNotification;

        protected void RaiseMessageNotification()
        {
            var handler = MessageNotification;
            if (handler != null)
            {
                handler(new MessageUpdateViewModel(this.ID, this.GetModel()));
                Console.WriteLine("MessageNotification Raised by " + this.GetType() + " ID is " + this.ID);
            }
        }


        public Guid ID { get; set; }
        //public MessageDispatcher MessageDispatcher { get; set; }


        //public string GetAddress() => $"{parentAddress}{Name}/";
        public abstract void SetViewModel(IModel model);
        public abstract IModel GetModel();
    }
}