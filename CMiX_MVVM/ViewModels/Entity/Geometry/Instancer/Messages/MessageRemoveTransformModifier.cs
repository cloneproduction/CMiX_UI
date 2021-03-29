using CMiX.MVVM.ViewModels.MessageService;
using CMiX.Studio.ViewModels.MessageService;
using System;

namespace CMiX.MVVM.ViewModels
{
    public class MessageRemoveTransformModifier : IMessage
    {
        public MessageRemoveTransformModifier(Guid componentID, int index)
        {
            Index = index;
            ComponentID = componentID;
        }

        public int Index { get; set; }
        public Guid ComponentID { get; set; }

        public void Process(MessageReceiver messageReceiver)
        {
            //Instancer instancer = messageProcessor as Instancer;
            ////instancer.TransformModifiers[Index].Dispose();
            //instancer.TransformModifiers.RemoveAt(Index);
        }
    }
}