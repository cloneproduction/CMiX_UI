using CMiX.MVVM.ViewModels.MessageService;
using System;

namespace CMiX.MVVM.ViewModels.Components.Messages
{
    public class MessageRemoveComponent : IComponentMessage
    {
        public MessageRemoveComponent()
        {

        }
        public MessageRemoveComponent(Guid id, int index)
        {
            ID = id;
            Index = index;
        }

        public int Index { get; set; }
        public object Obj { get; set; }
        public Guid ID { get; set; }


        public void Process(IMessageProcessor viewModel, IMessageTerminal messageTerminal)
        {
            Component component = viewModel as Component;
            component.Components[Index].Dispose();
            component.Components.RemoveAt(Index);
        }
    }
}
