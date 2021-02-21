using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.Mediator;
using CMiX.MVVM.ViewModels.MessageService;

namespace CMiX.MVVM.ViewModels
{
    public class MessageAddTransformModifier : IMessage
    {
        public MessageAddTransformModifier()
        {

        }

        public MessageAddTransformModifier(string address, TransformModifierNames transformModifierNames, ITransformModifierModel transformModifierModel)
        {
            Address = address;
            TransformModifierNames = transformModifierNames;
            TransformModifierModel = transformModifierModel;
        }

        public TransformModifierNames TransformModifierNames { get; set; }
        public ITransformModifierModel TransformModifierModel { get; set; }
        public string Address { get; set; }

        public void Process(IMessageProcessor messageProcessor)
        {
            Instancer instancer = messageProcessor as Instancer;
            var transformModifier = instancer.Factory.CreateTransformModifier(TransformModifierNames, TransformModifierModel, instancer);
            instancer.TransformModifiers.Add(transformModifier);
        }
    }
}