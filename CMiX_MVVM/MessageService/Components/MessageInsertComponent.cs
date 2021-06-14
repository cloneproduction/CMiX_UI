using CMiX.MVVM.Interfaces;
using CMiX.MVVM.MessageService;

namespace CMiX.MVVM.ViewModels.Components.Messages
{
    public class MessageInsertComponent : Message, IComponentMessage
    {
        public MessageInsertComponent()
        {

        }

        public MessageInsertComponent(IComponentModel componentModel, int index)
        {
            Index = index;
            this.ComponentModel = componentModel;
        }

        public int Index { get; set; }
        public IComponentModel ComponentModel { get; set; }

        public override void Process<T>(T receiver)
        {
            var component = receiver as Component;
        }
    }
}