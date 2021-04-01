using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels.MessageService;
using CMiX.Studio.ViewModels.MessageService;
using System;

namespace CMiX.MVVM.ViewModels.Components.Messages
{
    public class MessageInsertComponent : IComponentMessage
    {
        public MessageInsertComponent()
        {

        }
        public MessageInsertComponent(Guid componentID, IComponentModel componentModel, int index)
        {
            this.ComponentID = componentID;
            this.ComponentModel = componentModel;
        }

        private int Index { get; set; }
        public IComponentModel ComponentModel { get; set; }
        public object Obj { get; set; }
        public Guid ComponentID { get; set; }

        public void Process(IMessageDispatcher messageDispatcher)
        {
            //Component component = messageProcessor as Component;
            //var newComponent = component.CreateChild();// ComponentFactory.CreateComponent(ComponentModel);
            //component.Components.Insert(Index, newComponent);
        }
    }
}