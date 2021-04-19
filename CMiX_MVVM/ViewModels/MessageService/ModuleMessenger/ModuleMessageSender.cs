using System;

namespace CMiX.MVVM.ViewModels.MessageService.ModuleMessenger
{
    public class ModuleMessageSender : IMessageDispatcher
    {
        public ModuleMessageSender(Guid componentID)
        {
            ComponentID = componentID;
        }


        Guid ComponentID { get; set; }

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
                message.ComponentID = ComponentID;
                _nextHandler.ProcessMessage(message);
                Console.WriteLine("ModuleMessageSender SendMessage");
            }
        }
    }
}