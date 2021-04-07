using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels.MessageService;
using CMiX.MVVM.ViewModels.MessageService.Messages;
using CMiX.MVVM.ViewModels.MessageService.MessageSendCOR;
using System;

namespace CMiX.MVVM.ViewModels
{
    public abstract class MessageCommunicator : ViewModel, IHandler, IMessageProcessor
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


        public abstract void SetModuleReceiver(ModuleMessageDispatcher messageDispatcher);

        private IHandler _nextHandler;
        public void SendMessage(IMessage message)
        {
            if(_nextHandler != null)
            {
                _nextHandler.SendMessage(message);
            }
        }

        public IHandler SetNext(IHandler handler)
        {
            _nextHandler = handler;
            return handler;
        }


        public void ProcessMessage(IMessage message)
        {
            var vmMessage = message as IViewModelMessage;
            vmMessage.Process(this);
        }


        public Guid ID { get; set; }
        public abstract void SetViewModel(IModel model);
        public abstract IModel GetModel();
    }
}