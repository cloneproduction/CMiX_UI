using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels;

namespace CMiX.MVVM.MessageService
{
    public class MessageAddTransformModifier : Message, ITransformModifierMessage
    {
        public MessageAddTransformModifier()
        {

        }

        public MessageAddTransformModifier(ITransformModifierModel transformModifierModel)
        {
            TransformModifierModel = transformModifierModel;
        }

        public ITransformModifierModel TransformModifierModel { get; set; }

        public override void Process<T>(T receiver)
        {
            Instancer instancer = receiver as Instancer;
            var transformModifier = instancer.Factory.CreateTransformModifier(TransformModifierModel);
            instancer.AddTransformModifier(transformModifier);
            //instancer.CreateTransformModifier(TransformModifierModel.Name);
        }
    }
}