using CMiX.MVVM.ViewModels.Components;
using CMiX.MVVM.ViewModels.Components.Messages;
using CMiX.MVVM.ViewModels.MessageService.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.MVVM.ViewModels.MessageService.ModuleMessenger
{
    public class ComponentManagerMessageReceiver : IMessageReceiver, IMessageDispatcher
    {
        public ComponentManagerMessageReceiver()
        {

        }

        IMessageCommunicator MessageCommunicator { get; set; }

        public void RegisterReceiver (IMessageCommunicator messageCommunicator)
        {
            MessageCommunicator = messageCommunicator;
        }

        public void UnregisterReceiver(IMessageCommunicator component)
        {
            throw new NotImplementedException();
        }

        public void ReceiveMessage(IMessage message)
        {
            var componentManager = MessageCommunicator as ComponentManager;
            if (componentManager != null)
            {
                componentManager.ReceiveMessage(message);
            }
        }
    }
}