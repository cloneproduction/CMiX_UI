using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels.Components;
using CMiX.MVVM.ViewModels.Components.Messages;
using System;

namespace CMiX.MVVM.ViewModels.MessageService.ModuleMessenger
{
    public class ComponentMessageSender : IMessageDispatcher
    {
        public ComponentMessageSender()
        {

        }


        public IMessage SetMessageID(IMessage message)
        {
            return message;
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
                Console.WriteLine("ComponentMessageSender SendMessage");
            }
        }

        public void SendMessageAddComponent(Component component, Component newComponent)
        {
            if (_nextHandler != null)
            {
                _nextHandler.ProcessMessage(new MessageAddComponent(component.ID, newComponent.GetModel() as IComponentModel));
                Console.WriteLine("ManagerMessageSender SendMessage");
            }
        }

        public void SendMessageRemoveComponent(Component selectedParent, int index)
        {
            if (_nextHandler != null)
            {
                _nextHandler.ProcessMessage(new MessageRemoveComponent(selectedParent.ID, index));
                Console.WriteLine("ManagerMessageSender SendMessage");
            }
        }
    }
}