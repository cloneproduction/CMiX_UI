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
    public class ComponentManagerMessageReceiver : IMessageDispatcher
    {
        public ComponentManagerMessageReceiver()
        {

        }

        ComponentManager ComponentManager { get; set; }

        public void RegisterReceiver (ComponentManager componentManager)
        {
            ComponentManager = componentManager;
        }

        public void ProcessMessage(IMessage message)
        {
            var msg = message as IComponentManagerMessage;

            if (msg != null)
            {
                msg.Process(ComponentManager);
                return;
            }

            ComponentManager.MessageDispatcher.ProcessMessage(message);
        }
    }
}