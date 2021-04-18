using CMiX.MVVM.ViewModels.MessageService.MessageSendCOR;
using System;

namespace CMiX.MVVM.ViewModels.MessageService
{
    public class MessageDispatcherSender //: IMessageDispatcherSender 
    {
        public MessageDispatcherSender()
        {

        }


        public IMessage SetMessageID(IMessage message)
        {
            return message;
        }


        private IMessageSendHandler _nextHandler;
        public IMessageSendHandler SetNextSender(IMessageSendHandler handler)
        {
            _nextHandler = handler;
            return handler;
        }


        public void SendMessage(IMessage message)
        {
            if(_nextHandler != null)
            {
                _nextHandler.SendMessage(message);
                Console.WriteLine("MessageDispatcher SendMessage");
            }
        }
    }
}