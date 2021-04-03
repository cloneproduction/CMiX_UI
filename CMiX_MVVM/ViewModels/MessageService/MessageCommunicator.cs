using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels.MessageService;
using CMiX.MVVM.ViewModels.MessageService.Messages;
using System;

namespace CMiX.MVVM.ViewModels
{
    public abstract class MessageCommunicator : ViewModel, IMessageProcessor
    {

        public event Action<IMessage> MessageOutNotification;
        
        public void RaiseMessageNotification()
        {
            var handler = MessageOutNotification;
            if (handler != null)
            {
                handler(new MessageUpdateViewModel(this.ID, this.GetModel()));
                Console.WriteLine("MessageNotification Raised by " + this.GetType() + " ID is " + this.ID);
            }
        }


        public Guid ID { get; set; }
        public abstract void SetViewModel(IModel model);
        public abstract IModel GetModel();
    }
}