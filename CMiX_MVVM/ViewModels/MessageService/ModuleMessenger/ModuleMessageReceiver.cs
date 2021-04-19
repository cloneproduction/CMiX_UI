using System;
using System.Collections.Generic;

namespace CMiX.MVVM.ViewModels.MessageService
{
    public class ModuleMessageReceiver : IMessageDispatcher
    {
        public ModuleMessageReceiver()
        {
            Modules = new Dictionary<Guid, Module>();
        }

        private Dictionary<Guid, Module> Modules { get; set; }

        private Module GetMessageProcessor(Guid id)
        {
            if (Modules.ContainsKey(id))
                return Modules[id];
            return null;
        }

        public void RegisterMessageReceiver(Module module)
        {
            if (Modules.ContainsKey(module.ID))
                Modules[module.ID] = module;
            else
                Modules.Add(module.ID, module);
        }

        public void UnregisterMessageReceiver(Module module)
        {
            Modules.Remove(module.ID);
        }

        public void ProcessMessage(IMessage message)
        {
            Console.WriteLine("ComponentMessageReceiver");
        }
    }
}