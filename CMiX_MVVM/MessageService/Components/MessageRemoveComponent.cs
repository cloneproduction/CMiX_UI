using CMiX.MVVM.MessageService;

namespace CMiX.MVVM.ViewModels.Components.Messages
{
    public class MessageRemoveComponent : Message, IComponentMessage
    {
        public MessageRemoveComponent()
        {

        }
        public MessageRemoveComponent(int index)
        {
            Index = index;
        }

        public int Index { get; set; }
    }
}
