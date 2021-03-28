using CMiX.MVVM.Models;

namespace CMiX.MVVM.ViewModels.MessageService.Messages
{
    public class MessageAddTransformModifier : IViewModelMessage
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