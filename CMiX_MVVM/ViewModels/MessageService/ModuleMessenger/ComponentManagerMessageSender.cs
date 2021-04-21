using System;


namespace CMiX.MVVM.ViewModels.MessageService.ModuleMessenger
{
    public class ComponentManagerMessageSender : IMessageSender, IMessageDispatcher
    {
        public ComponentManagerMessageSender()
        {

        }

        private IMessageDispatcher _nextHandler;
        public IMessageDispatcher SetSender(IMessageDispatcher handler)
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