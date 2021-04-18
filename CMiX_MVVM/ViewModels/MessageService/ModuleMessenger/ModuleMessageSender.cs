using System;

namespace CMiX.MVVM.ViewModels.MessageService.ModuleMessenger
{
    public class ModuleMessageSender : IMessageDispatcher
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

        public void ProcessMessage(IMessage message)
        {
            if (_nextHandler != null)
            {
                _nextHandler.ProcessMessage(message);
                Console.WriteLine("ModuleMessageSender SendMessage");
            }
        }
    }
}