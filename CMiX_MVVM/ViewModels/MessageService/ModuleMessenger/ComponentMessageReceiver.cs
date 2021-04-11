using CMiX.MVVM.ViewModels.Components;
using CMiX.MVVM.ViewModels.MessageService.MessageSendCOR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.MVVM.ViewModels.MessageService.ModuleMessenger
{
    public class ComponentMessageReceiver
    {
        public ComponentMessageReceiver()
        {
            MessageProcessors = new Dictionary<Guid, Component>();
        }

        private Dictionary<Guid, Component> MessageProcessors { get; set; }

        public void RegisterMessageReceiver(Component messageProcessor)
        {
            if (MessageProcessors.ContainsKey(messageProcessor.ID))
                MessageProcessors[messageProcessor.ID] = messageProcessor;
            else
                MessageProcessors.Add(messageProcessor.ID, messageProcessor);
        }

        public void UnregisterMessageReceiver(Component messageProcessor)
        {
            MessageProcessors.Remove(messageProcessor.ID);
        }

        public void ReceiveMessage(IMessage message)
        {

        }
    }
}
