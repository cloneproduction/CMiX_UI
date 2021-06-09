using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.Beat;

namespace CMiX.MVVM.ViewModels
{
    public class TransformModifierFactory
    {
        public TransformModifierFactory(MasterBeat masterBeat)
        {
            this.MasterBeat = masterBeat;
        }

        public MasterBeat MasterBeat { get; set; }

        public ITransformModifier CreateTransformModifier(TransformModifierNames transformModifierNames)
        {
            ITransformModifier transformModifier = null;

            switch (transformModifierNames)
            {
                case TransformModifierNames.Randomized:
                    transformModifier = CreateRandomized();
                    break;
            }

            return transformModifier;
        }

        public ITransformModifier CreateTransformModifier(ITransformModifierModel transformModifierModel)
        {
            ITransformModifier transformModifier = null;

            switch (transformModifierModel.Name)
            {
                case TransformModifierNames.Randomized:
                    transformModifier = CreateRandomized(transformModifierModel as RandomXYZModel);
                    break;
            }

            return transformModifier;
        }


        private RandomXYZ CreateRandomized()
        {
            var randomized = new RandomXYZ(new RandomXYZModel(), this.MasterBeat);
            return randomized;
        }

        private RandomXYZ CreateRandomized(RandomXYZModel randomXYZModel)
        {
            var randomized = new RandomXYZ(randomXYZModel, this.MasterBeat);
            return randomized;
        }
    }
}
