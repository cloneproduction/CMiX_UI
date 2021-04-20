using CMiX.MVVM.ViewModels.Components.Messages;
using CMiX.MVVM.ViewModels.MessageService.MessageSendCOR;
using System;
using System.Collections.Generic;

namespace CMiX.MVVM.ViewModels.MessageService
{
    public class MessageDispatcherReceiver //: IMessageDispatcherReceiver
    {
        public MessageDispatcherReceiver()
        {
            MessageProcessors = new Dictionary<Guid, IMessageReceiveHandler>();
        }


        public Dictionary<Guid, IMessageReceiveHandler> MessageProcessors { get; set; }

        public IMessageReceiveHandler GetMessageProcessor(Guid id)
        {
            if (MessageProcessors.ContainsKey(id))
                return MessageProcessors[id];
            return null;
        }


        public void ReceiveMessage(IMessage message)
        {
            if (message is IComponentManagerMessage)
            {
                ///OnMessageInNotification(message);
            }
            else
            {
                var messageProcessor = GetMessageProcessor(message.ComponentID);
                if(messageProcessor != null)
                    messageProcessor.ReceiveMessage(message);
            }
        }


        public void RegisterReceiver(IMessageReceiveHandler messageProcessor)
        {
            //if (MessageProcessors.ContainsKey(messageProcessor.ID))
            //    MessageProcessors[messageProcessor.ID] = messageProcessor;
            //else
            //    MessageProcessors.Add(messageProcessor.ID, messageProcessor);
        }


        public void UnregisterMessageReceiver(IMessageReceiveHandler messageProcessor)
        {
            //MessageProcessors.Remove(messageProcessor.ID);
        }
    }
}