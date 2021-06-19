using CMiX.Core.Models;
using CMiX.Core.Presentation.ViewModels;

namespace CMiX.Core.MessageService
{
    public class MessageAddTransformModifier : Message
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
        }
    }
}