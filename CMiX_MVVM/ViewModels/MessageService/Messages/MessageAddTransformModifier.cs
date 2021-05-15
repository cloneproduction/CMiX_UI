using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels;
using System;

namespace CMiX.MVVM.MessageService
{
    public class MessageAddTransformModifier //: IViewModelMessage
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

        //public void Process(IMessageProcessor messageProcessor)
        //{
        //    //Instancer instancer = messageProcessor as Instancer;
        //    //var transformModifier = instancer.Factory.CreateTransformModifier(TransformModifierNames, TransformModifierModel, instancer);
        //    //instancer.TransformModifiers.Add(transformModifier);
        //}
    }
}