namespace CMiX.MVVM.ViewModels.MessageService.Messages
{
    public class MessageRemoveComponent : IMessage
    {
        public MessageRemoveComponent()
        {

        }
        public MessageRemoveComponent(string address, int index)
        {
            Address = address;
            Index = index;
        }

        private int Index { get; set; }
        public object Obj { get; set; }
        public string Address { get; set; }

        public void Process(ISenderTest viewModel)
        {
            Component component = viewModel as Component;
            component.Components[Index].Dispose();
            component.Components.RemoveAt(Index);
        }
    }
}
