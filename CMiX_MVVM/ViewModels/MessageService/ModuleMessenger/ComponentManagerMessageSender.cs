using System;

namespace CMiX.MVVM.ViewModels.MessageService.ModuleMessenger
{
    public class ComponentManagerMessageSender : IMessageDispatcher
    {
        public ComponentManagerMessageSender()
        {

        }

        private IMessageDispatcher _nextHandler;
        public IMessageDispatcher SetNextSender(IMessageDispatcher handler)
        {
            _nextHandler = handler;
            return handler;
        }

        public void ProcessMessage(IMessage message)
        {
            if (_nextHandler != null)
            {
                _nextHandler.ProcessMessage(message);
                Console.WriteLine("ManagerMessageSender SendMessage");
            }
        }
    }
}