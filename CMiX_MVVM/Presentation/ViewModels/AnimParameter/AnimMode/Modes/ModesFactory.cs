﻿namespace CMiX.Core.Presentation.ViewModels
{
    public static class ModesFactory
    {
        public static IAnimMode CreateMode(ModeType modeType, AnimParameter animParameter)
        {
            IAnimMode animMode = null;

            if (modeType == ModeType.Steady)
                animMode = CreateSteady(animParameter);
            else if (modeType == ModeType.LFO)
                animMode = CreateLFO(animParameter);
            else if (modeType == ModeType.Random)
                animMode = CreateRandomized(animParameter);
            else if (modeType == ModeType.Stepper)
                animMode = CreateStepper(animParameter);
            else if (modeType == ModeType.None)
                animMode = CreateNone(animParameter);

            return animMode;
        }

        private static None CreateNone(AnimParameter animParameter)
        {
            return null; // new None(animParameter);
        }

        private static LFO CreateLFO(AnimParameter animParameter)
        {
            return null; //return new LFO(animParameter);
        }

        private static Steady CreateSteady(AnimParameter animParameter)
        {
            return null; //return new Steady(animParameter);
        }

        private static Stepper CreateStepper(AnimParameter animParameter)
        {
            return null; //return new Stepper(animParameter);
        }

        private static Randomized CreateRandomized(AnimParameter animParameter)
        {
            return null; //return new Randomized(animParameter);
        }
    }
}