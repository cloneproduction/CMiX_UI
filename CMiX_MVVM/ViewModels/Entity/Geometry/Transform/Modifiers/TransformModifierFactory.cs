using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.MVVM.ViewModels
{
    public class TransformModifierFactory
    {
        public TransformModifierFactory(MasterBeat masterBeat)
        {
            this.MasterBeat = masterBeat;
        }

        public MasterBeat MasterBeat { get; set; }
        public ITransformModifier CreateTransformModifier(TransformModifierNames transformModifierNames, Sender parentSender)
        {
            ITransformModifier transformModifier = null;

            switch (transformModifierNames)
            {
                case TransformModifierNames.TranslateXYZ:
                    transformModifier = CreateTranslateXYZ(parentSender, this.MasterBeat);
                    break;
                case TransformModifierNames.Randomized:
                    transformModifier = CreateRandomized(parentSender, this.MasterBeat);
                    break;
            }

            return transformModifier;
        }

        private TranslateModifier CreateTranslateXYZ(Sender parentSender, MasterBeat masterBeat)
        {
            return new TranslateModifier(nameof(TranslateModifier), parentSender, masterBeat);
        }

        private Randomized CreateRandomized(Sender parentSender, MasterBeat masterBeat)
        {
            return new Randomized(nameof(TranslateModifier), parentSender);
        }
    }
}
