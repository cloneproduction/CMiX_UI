using CMiX.MVVM.ViewModels.MessageService;
using CMiX.Studio.ViewModels.MessageService;
using System;

namespace CMiX.MVVM.ViewModels.Components.Messages
{
    public class MessageRemoveComponent : IComponentMessage
    {
        public MessageRemoveComponent()
        {

        }
        public MessageRemoveComponent(Guid componentID, int index)
        {
            ComponentID = componentID;
            Index = index;
        }

        public int Index { get; set; }
        public object Obj { get; set; }
        public Guid ComponentID { get; set; }


        public void Process(IMessageProcessor messageProcessor)
        {
            //IComponentMessageProcessor messageProcessor;
            //if (messageReceiver.MessageProcessors.TryGetValue(ComponentID, out messageProcessor))
            //{
            //    Component component = messageProcessor as Component;
            //    component.Components[Index].Dispose();
            //    component.Components.RemoveAt(Index);
            //}
        }
    }
}
