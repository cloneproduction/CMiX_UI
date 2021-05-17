using CMiX.MVVM.Interfaces;
using CMiX.MVVM.MessageService;
using System;

namespace CMiX.MVVM.ViewModels.Components.Messages
{
    public class MessageInsertComponent : Message, IComponentMessage
    {
        public MessageInsertComponent()
        {

        }

        public MessageInsertComponent(Guid id, IComponentModel componentModel, int index)
        {
            this.AddID(id);

            Index = index;
            this.ComponentModel = componentModel;
        }

        private int Index { get; set; }
        public IComponentModel ComponentModel { get; set; }
        public object Obj { get; set; }
    }
}