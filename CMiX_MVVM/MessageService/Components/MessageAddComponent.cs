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

        public MessageAddComponent(Component component)
        {
            ComponentModel = component.GetModel() as IComponentModel;
        }

        public IComponentModel ComponentModel { get; set; } // must be public because of Ceras...
    }
}