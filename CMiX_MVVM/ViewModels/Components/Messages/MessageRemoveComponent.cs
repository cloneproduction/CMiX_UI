using CMiX.MVVM.ViewModels.MessageService;

namespace CMiX.MVVM.ViewModels.Components.Messages
{
    public class MessageRemoveComponent : IComponentMessage
    {
        public MessageRemoveComponent()
        {

        }
        public MessageRemoveComponent(string address, int index)
        {
            Address = address;
            Index = index;
        }

        public int Index { get; set; }
        public object Obj { get; set; }
        public string Address { get; set; }


        public void Process(IMessageProcessor messageProcessor)
        {
            Component component = messageProcessor as Component;
            component.Components[Index].Dispose();
            component.Components.RemoveAt(Index);
        }

        public void Process(IMessageProcessor viewModel, IMessageTerminal messageTerminal)
        {
            throw new System.NotImplementedException();
        }
    }
}
