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
                //case TransformModifierNames.ScaleXYZ:
                //    break;
                //case TransformModifierNames.RotateXYZ:
                //    break;
                //default:
                //    break;
            }

            return transformModifier;
        }

        private TranslateModifier CreateTranslateXYZ(Sender parentSender, MasterBeat masterBeat)
        {
            return new TranslateModifier(nameof(TranslateModifier), parentSender, masterBeat);
        }
    }
}
