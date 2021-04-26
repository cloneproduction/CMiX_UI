using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels.MessageService;
using System;
using System.Collections.Generic;

namespace CMiX.MVVM.ViewModels.Components.Messages
{
    [Serializable]
    public class MessageAddComponent : IComponentMessage
    {
        public MessageAddComponent()
        {

        }

        public MessageAddComponent(Guid componentID, IComponentModel componentModel)
        {
            ComponentID = componentID;
            ComponentModel = componentModel;
        }

        
        public Guid ComponentID { get; set; }
        public IComponentModel ComponentModel { get; set; } // must be public because of Ceras...
    }
}