using CMiX.MVVM.ViewModels.Components;
using System;
using System.Collections.Generic;

namespace CMiX.MVVM.ViewModels.MessageService.ModuleMessenger
{
    public class ComponentMessageReceiver : IMessageDispatcher
    {
        public ComponentMessageReceiver()
        {
            Components = new Dictionary<Guid, Component>();
        }

        private Dictionary<Guid, Component> Components { get; set; }

        public Component GetMessageProcessor(Guid id)
        {
            if (Components.ContainsKey(id))
                return Components[id];
            return null;
        }

        public void RegisterMessageReceiver(Component component)
        {
            if (Components.ContainsKey(component.ID))
                Components[component.ID] = component;
            else
                Components.Add(component.ID, component);
        }

        public void UnregisterMessageReceiver(Component component)
        {
            Components.Remove(component.ID);
        }

        public void ProcessMessage(IMessage message)
        {
            var component = GetMessageProcessor(message.ComponentID);
            if(component != null)
            {
                component.MessageDispatcher.ProcessMessage(message);
            }
            Console.WriteLine("ComponentMessageReceiver");
        }
    }
}