using CMiX.MVVM.ViewModels.MessageService.Messages;
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

        public void RegisterReceiver(Module module)
        {
            if (Modules.ContainsKey(module.ID))
                Modules[module.ID] = module;
            else
                Modules.Add(module.ID, module);
        }

        public void UnregisterReceiver(Module module)
        {
            Modules.Remove(module.ID);
        }

        public void ProcessMessage(IMessage message)
        {
            var msg = message as MessageUpdateViewModel;
            var module = GetMessageProcessor(msg.ModuleID);

            if (msg != null && module != null)
            {
                module.ReceiveViewModelUpdate(msg.Model);
            }
        }
    }
}