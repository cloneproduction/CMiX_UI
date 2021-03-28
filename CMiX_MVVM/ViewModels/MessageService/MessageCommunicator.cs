using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.MessageService;
using CMiX.MVVM.ViewModels.MessageService.Messages;
using System;

namespace CMiX.MVVM.ViewModels
{
    public abstract class MessageCommunicator : ViewModel, IMessageProcessor
    {
        public MessageCommunicator(MessageDispatcher messageDispatcher, Model model)
        {
            ID = model.ID;
            MessageDispatcher = messageDispatcher;
            messageDispatcher.RegisterColleague(this);
        }

        public MessageDispatcher MessageDispatcher { get; set; }

        public event Action<IMessage> MessageNotification;

        protected void RaiseMessageNotification()
        {
            var handler = MessageNotification;
            if (handler != null)
            {
                handler(new MessageUpdateViewModel(this.MessageDispatcher.ComponentID, this.ID, this.GetModel()));
                Console.WriteLine("MessageNotification Raised by " + this.GetType() + " ID is " + this.ID);
            }
        }


        public Guid ID { get; set; }
        public abstract void SetViewModel(IModel model);
        public abstract IModel GetModel();
    }
}