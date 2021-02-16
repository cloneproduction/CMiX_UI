namespace CMiX.MVVM.ViewModels.MessageService.Messages
{
    public class MessageMoveComponent : IMessage
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

        private int OldIndex { get; set; }
        private int NewIndex { get; set;
        }
        public object Obj { get; set; }
        public string Address { get; set; }

        public void Process(ISenderTest viewModel)
        {
            Component component = viewModel as Component;
            component.Components.Move(OldIndex, NewIndex);
        }
    }
}
