using CMiX.MVVM.ViewModels.MessageService;

namespace CMiX.MVVM.ViewModels
{
    public class MessageRemoveTransformModifier : IMessage
    {
        public MessageRemoveTransformModifier(string address, int index)
        {
            Index = index;
            Address = address;
        }

        public int Index { get; set; }
        public string Address { get; set; }

        public void Process(IMessageProcessor messageProcessor)
        {
            Instancer instancer = messageProcessor as Instancer;
            instancer.TransformModifiers[Index].Dispose();
            instancer.TransformModifiers.RemoveAt(Index);
        }
    }
}