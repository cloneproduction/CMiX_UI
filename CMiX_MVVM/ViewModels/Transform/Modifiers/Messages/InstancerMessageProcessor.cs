using CMiX.MVVM.MessageService;
using System;

namespace CMiX.MVVM.ViewModels.Modifiers
{
    public class InstancerMessageProcessor : IMessageProcessor
    {
        public InstancerMessageProcessor(Instancer instancer)
        {
            Instancer = instancer;
        }

        private Instancer Instancer { get; set; }
        public Guid GetID()
        {
            return Instancer.ID;
        }

        public void ProcessMessage(Message message)
        {
            if (message is MessageAddTransformModifier)
            {
                ReceiveMessageAddTransformModifier(message as MessageAddTransformModifier);
                return;
            }

            //if (message is MessageRemoveComponent)
            //{
            //    ReceiveMessageRemoveComponent(message as MessageRemoveComponent);
            //    return;
            //}
        }

        private void ReceiveMessageAddTransformModifier(MessageAddTransformModifier messageAddTransformModifier)
        {
            ITransformModifier transformModifier = Instancer.Factory.CreateTransformModifier(messageAddTransformModifier.TransformModifierModel);
            transformModifier.SetReceiver(Instancer.MessageReceiver);
            Instancer.AddTransformModifier(transformModifier);
            Console.WriteLine("ReceiveMessageAddTransformModifier Count is " + Instancer.TransformModifiers.Count);
        }
    }
}