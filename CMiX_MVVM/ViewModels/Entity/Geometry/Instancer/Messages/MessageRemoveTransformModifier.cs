using CMiX.MVVM.ViewModels.MessageService;
using System;

namespace CMiX.MVVM.ViewModels
{
    public class MessageRemoveTransformModifier : IMessage
    {
        public MessageRemoveTransformModifier(Guid id, int index)
        {
            Index = index;
            ID = id;
        }

        public int Index { get; set; }
        public Guid ID { get; set; }

        public void Process(IMessageProcessor messageProcessor)
        {
            Instancer instancer = messageProcessor as Instancer;
            //instancer.TransformModifiers[Index].Dispose();
            instancer.TransformModifiers.RemoveAt(Index);
        }
    }
}