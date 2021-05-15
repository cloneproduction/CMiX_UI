using CMiX.MVVM.Interfaces;
using CMiX.MVVM.MessageService;
using System;
using System.Collections.Generic;

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

        public List<Guid> IDs { get; set; }
        private int Index { get; set; }
        public IComponentModel ComponentModel { get; set; }
        public object Obj { get; set; }
        public Guid ComponentID { get; set; }
    }
}