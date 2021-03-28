using CMiX.MVVM.Models;
using System;

namespace CMiX.MVVM.ViewModels.MessageService.Messages
{
    public class MessageAddTransformModifier : IViewModelMessage
    {
        public MessageAddTransformModifier()
        {

        }

        public MessageAddTransformModifier(Guid id, TransformModifierNames transformModifierNames, ITransformModifierModel transformModifierModel)
        {
            ID = id;
            TransformModifierNames = transformModifierNames;
            TransformModifierModel = transformModifierModel;
        }

        public TransformModifierNames TransformModifierNames { get; set; }
        public ITransformModifierModel TransformModifierModel { get; set; }
        public Guid ID { get; set; }
        public Guid ComponentID { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Process(IMessageProcessor messageProcessor)
        {
            Instancer instancer = messageProcessor as Instancer;
            var transformModifier = instancer.Factory.CreateTransformModifier(TransformModifierNames, TransformModifierModel, instancer);
            instancer.TransformModifiers.Add(transformModifier);
        }
    }
}