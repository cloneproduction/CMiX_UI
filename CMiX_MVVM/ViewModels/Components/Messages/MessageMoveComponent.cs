using CMiX.MVVM.ViewModels.MessageService;
using System;

namespace CMiX.MVVM.ViewModels.Components.Messages
{
    public class MessageMoveComponent : IComponentMessage
    {
        public MessageMoveComponent()
        {

        }
        public MessageMoveComponent(Guid id, int oldIndex, int newIndex)
        {
            ID = id;
            OldIndex = oldIndex;
            NewIndex = newIndex;
        }

        public int OldIndex { get; set; }
        public int NewIndex { get; set; }
        public Guid ID { get; set; }
        

        public void Process(IMessageProcessor messageProcessor)
        {
            Component component = messageProcessor as Component;
            component.Components.Move(OldIndex, NewIndex);
        }

        public void Process(IMessageProcessor viewModel, IMessageTerminal messageTerminal)
        {
            throw new System.NotImplementedException();
        }
    }
}
