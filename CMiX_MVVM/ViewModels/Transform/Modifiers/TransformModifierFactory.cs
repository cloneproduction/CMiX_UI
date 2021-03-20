﻿using CMiX.MVVM.Models;
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
        private MessageTerminal MessageTerminal { get; set; }

        public MasterBeat MasterBeat { get; set; }

        public ITransformModifier CreateTransformModifier(TransformModifierNames transformModifierNames, MessageCommunicator parentSender)
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

        public ITransformModifier CreateTransformModifier(TransformModifierNames transformModifierNames, ITransformModifierModel transformModifierModel, MessageCommunicator parentSender)
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

        private RandomXYZ CreateRandomized(MessageCommunicator parentSender)
        {
            ID++;
            return new RandomXYZ(nameof(TranslateModifier), parentSender, ID, this.MasterBeat, new RandomXYZModel());
        }
    }
}
