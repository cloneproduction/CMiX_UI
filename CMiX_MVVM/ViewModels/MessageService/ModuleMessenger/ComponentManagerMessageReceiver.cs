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

        public void RegisterMessageReceiver (ComponentManager componentManager)
        {
            ComponentManager = componentManager;
        }

        public void ProcessMessage(IMessage message)
        {
            if(message is MessageAddComponent)
            {
                var msg = message as MessageAddComponent;
                msg.Process(ComponentManager);
            }
            else if (message is MessageUpdateViewModel)
            {
                ComponentManager.MessageDispatcher.ProcessMessage(message);
            }

            Console.WriteLine("ComponentManagerMessageReceiver ProcessMessage");
        }
    }
}