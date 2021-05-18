using CMiX.MVVM.Interfaces;
using CMiX.MVVM.MessageService;
using System;
using System.Collections.Generic;

namespace CMiX.MVVM.ViewModels.Components.Messages
{
    [Serializable]
    public class MessageAddComponent : Message, IComponentMessage
    {
        public MessageAddComponent()
        {

        }

        public MessageAddComponent(IComponentModel componentModel)
        {
            ComponentModel = componentModel;
        }

        public IComponentModel ComponentModel { get; set; } // must be public because of Ceras...
    }
}