using CMiX.MVVM.ViewModels.Mediator;

namespace CMiX.MVVM.ViewModels.Components.Messages
{
    public class MessageMoveComponent : IComponentMessage
    {
        public MessageMoveComponent()
        {

        }
        public MessageMoveComponent(string address, int oldIndex, int newIndex)
        {
            Address = address;
            OldIndex = oldIndex;
            NewIndex = newIndex;
        }

        public int OldIndex { get; set; }
        public int NewIndex { get; set; }
        public string Address { get; set; }
        

        public void Process(IMessageProcessor messageProcessor)
        {
            Component component = messageProcessor as Component;
            component.Components.Move(OldIndex, NewIndex);
        }
    }
}
