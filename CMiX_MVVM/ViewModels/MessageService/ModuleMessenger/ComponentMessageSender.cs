using CMiX.MVVM.ViewModels.MessageService.MessageSendCOR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.MVVM.ViewModels.MessageService.ModuleMessenger
{
    public class ComponentMessageSender
    {
        public ComponentMessageSender()
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
            if (_nextHandler != null)
            {
                _nextHandler.SendMessage(message);
                Console.WriteLine("MessageDispatcher SendMessage");
            }
        }
    }
}
