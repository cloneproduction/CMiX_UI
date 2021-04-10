using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels.MessageService;
using CMiX.MVVM.ViewModels.MessageService.Messages;
using CMiX.MVVM.ViewModels.MessageService.MessageSendCOR;
using System;

namespace CMiX.MVVM.ViewModels
{
    public abstract class MessageCommunicator : ViewModel, IMessageSendHandler, IMessageReceiveHandler
    {
        private IMessageSendHandler _nextHandler;
        public IMessageSendHandler SetNextSender(IMessageSendHandler handler)
        {
            _nextHandler = handler;
            return handler;
        }

        public void SendMessage(IMessage message)
        {
            if (_nextHandler != null)
            {
                _nextHandler.SendMessage(message);
            }
        }



        public abstract void SetModuleReceiver(ModuleMessageDispatcher messageDispatcher);

        public void ReceiveMessage(IMessage message)
        {
            var vmMessage = message as IViewModelMessage;
            vmMessage.Process(this);
        }


        public Guid ID { get; set; }
        public abstract void SetViewModel(IModel model);
        public abstract IModel GetModel();
    }
}