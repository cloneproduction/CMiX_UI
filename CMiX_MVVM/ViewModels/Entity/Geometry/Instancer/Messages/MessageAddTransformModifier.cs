using CMiX.MVVM.Models;
using CMiX.Studio.ViewModels.MessageService;
using System;

namespace CMiX.MVVM.ViewModels.MessageService.Messages
{
    public class MessageAddTransformModifier : IViewModelMessage
    {
        public MessageAddTransformModifier()
        {

        }

        public MessageAddTransformModifier(Guid componentID, TransformModifierNames transformModifierNames, ITransformModifierModel transformModifierModel)
        {
            ComponentID = componentID;
            TransformModifierNames = transformModifierNames;
            TransformModifierModel = transformModifierModel;
        }

        public TransformModifierNames TransformModifierNames { get; set; }
        public ITransformModifierModel TransformModifierModel { get; set; }
        public Guid ComponentID { get; set; }
        public Guid ModuleID { get; set; }

        public void Process(MessageReceiver messageReceiver)
        {
            //Instancer instancer = messageProcessor as Instancer;
            //var transformModifier = instancer.Factory.CreateTransformModifier(TransformModifierNames, TransformModifierModel, instancer);
            //instancer.TransformModifiers.Add(transformModifier);
        }
    }
}