using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels.MessageService;
using CMiX.MVVM.ViewModels.MessageService.Messages;
using CMiX.MVVM.ViewModels.MessageService.MessageSendCOR;
using System;

namespace CMiX.MVVM.ViewModels
{
    public abstract class MessageCommunicator : ViewModel, IHandler, IMessageProcessor
    {
        public abstract void SetModuleReceiver(ModuleMessageDispatcher messageDispatcher);



        private IHandler _nextHandler;
        public void SendMessage(IMessage message)
        {
            if(_nextHandler != null)
            {
                _nextHandler.SendMessage(message);
            }
        }

        public IHandler SetNextSender(IHandler handler)
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