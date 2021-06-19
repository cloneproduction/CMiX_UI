using CMiX.Core.Interfaces;
using CMiX.Core.Presentation.ViewModels.Components;

namespace CMiX.Core.Network.Messages
{
    public class MessageInsertComponent : Message
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