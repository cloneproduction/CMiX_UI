using CMiX.MVVM.ViewModels.MessageService.MessageSendCOR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.MVVM.ViewModels.MessageService.ModuleMessenger
{
    public class ModuleMessageSender
    {
        public ModuleMessageSender()
        {

        }

        public IMessage SetMessageID(IMessage message)
        {
            return message;
        }

        private ComponentMessageSender _nextHandler;
        public ComponentMessageSender SetNextSender(ComponentMessageSender handler)
        {
            _nextHandler = handler;
            return handler;
        }

        public void SendMessage(IMessage message)
        {
            if (_nextHandler != null)
            {
                _nextHandler.SendMessage(message);
                Console.WriteLine("ModuleMessageSender SendMessage");
            }
        }
    }
}