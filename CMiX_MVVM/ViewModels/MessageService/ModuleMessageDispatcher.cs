using CMiX.MVVM.ViewModels.Components;
using CMiX.MVVM.ViewModels.MessageService.Messages;
using CMiX.MVVM.ViewModels.MessageService.MessageSendCOR;
using System;
using System.Collections.Generic;

namespace CMiX.MVVM.ViewModels.MessageService
{
    public class ModuleMessageDispatcher : IHandler
    {

        public ModuleMessageDispatcher(Component parentComponent)
        {
            ParentComponent = parentComponent;
            MessageProcessors = new Dictionary<Guid, IMessageProcessor>();
        }


        private Component ParentComponent { get; set; }
        public Dictionary<Guid, IMessageProcessor> MessageProcessors { get; set; }


        public event Action<IMessage> MessageOutNotification;
        public event Action<IMessage> MessageInNotification;


        public IMessageProcessor GetMessageProcessor(Guid id)
        {
            if (MessageProcessors.ContainsKey(id))
                return MessageProcessors[id];
            return null;
        }


        public void OnMessageInNotification(IMessage message)
        {
            throw new NotImplementedException();
        }

        public void OnMessageOutNotification(IMessage message)
        {
            var handler = MessageOutNotification;
            if (handler != null)
            {
                handler(message);
            }
        }


        public void ProcessMessage(IMessage message)
        {

        }


        public void SendChange(Func<IMessage> createMessage)
        {
            //var message = createMessage();
            //ParentComponent.MessageDispatcher.SendMessage(createMessage);
        }


        private IHandler _nextHandler;
        public IHandler SetNext(IHandler handler)
        {
            _nextHandler = handler;
            return handler;
        }

        public void SendMessage(IMessage message)
        {
            if(_nextHandler != null)
            {
                message.ComponentID = ParentComponent.ID;
                _nextHandler.SendMessage(message);
                Console.WriteLine("ModuleMessageDispatcher POUETPOUET");
            }
            
            //message.ComponentID = ParentComponent.ID;
            //ParentComponent.MessageDispatcher.SendMessage(message);
        }




        public void RegisterMessageProcessor(IMessageProcessor messageProcessor)
        {
            if (MessageProcessors.ContainsKey(messageProcessor.ID))
                MessageProcessors[messageProcessor.ID] = messageProcessor;
            else
                MessageProcessors.Add(messageProcessor.ID, messageProcessor);
        }

        public void UnregisterMessageProcessor(IMessageProcessor messageProcessor)
        {
            MessageProcessors.Remove(messageProcessor.ID);
        }


    }
}