using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels.MessageService;
using CMiX.MVVM.ViewModels.MessageService.Messages;
using CMiX.MVVM.ViewModels.MessageService.MessageSendCOR;
using CMiX.MVVM.ViewModels.MessageService.ModuleMessenger;
using System;

namespace CMiX.MVVM.ViewModels
{
    public abstract class MessageCommunicator : ViewModel//, IMessageSendHandler, IMessageReceiveHandler
    {
        private ModuleMessageSender _nextHandler;
        public ModuleMessageSender SetNextSender(ModuleMessageSender handler)
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


        public abstract void SetModuleReceiver(ModuleMessageReceiver messageDispatcher);

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