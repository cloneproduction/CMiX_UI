using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.Beat;
using CMiX.MVVM.ViewModels.MessageService;

namespace CMiX.MVVM.ViewModels
{
    public class TransformModifierFactory
    {
        public TransformModifierFactory(MasterBeat masterBeat)
        {
            this.MasterBeat = masterBeat;
        }

        private static int ID = 0;

        public MasterBeat MasterBeat { get; set; }

        public ITransformModifier CreateTransformModifier(TransformModifierNames transformModifierNames, Module parentSender)
        {
            ITransformModifier transformModifier = null;

            switch (transformModifierNames)
            {
                case TransformModifierNames.Randomized:
                    transformModifier = CreateRandomized(parentSender);
                    break;
            }

            return transformModifier;
        }

        public ITransformModifier CreateTransformModifier(TransformModifierNames transformModifierNames, ITransformModifierModel transformModifierModel, Module parentSender)
        {
            ITransformModifier transformModifier = null;

            switch (transformModifierNames)
            {
                case TransformModifierNames.Randomized:
                    transformModifier = CreateRandomized(parentSender);
                    break;
            }

            return transformModifier;
        }

        private RandomXYZ CreateRandomized(Module parentSender)
        {
            ID++;
            return null; //return new RandomXYZ(nameof(TranslateModifier), parentSender.me, ID, this.MasterBeat, new RandomXYZModel());
        }
    }
}
