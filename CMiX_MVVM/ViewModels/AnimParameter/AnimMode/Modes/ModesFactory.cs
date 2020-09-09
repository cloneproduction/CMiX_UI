namespace CMiX.MVVM.ViewModels
{
    public static class ModesFactory
    {
        public static AnimMode CreateMode(ModeType modeType, AnimParameter animParameter, double defaultValue, Sendable parent)
        {
            AnimMode animMode = null;

            if (modeType == ModeType.Steady)
                animMode = CreateSteady(animParameter, parent);
            else if (modeType == ModeType.LFO)
                animMode = CreateLFO(animParameter, parent);
            else if (modeType == ModeType.Random)
                animMode = CreateRandomized(animParameter, parent);
            else if (modeType == ModeType.Stepper)
                animMode = CreateStepper(animParameter, parent);
            else if (modeType == ModeType.None)
                animMode = CreateNone(animParameter, parent);

            return animMode;
        }

        private static None CreateNone(AnimParameter animParameter, Sendable parent)
        {
            return new None(animParameter, parent);
        }

        private static LFO CreateLFO(AnimParameter animParameter, Sendable parent)
        {
            return new LFO(animParameter, parent);
        }

        private static Steady CreateSteady(AnimParameter animParameter, Sendable parent)
        {
            return new Steady(animParameter, parent);
        }

        private static Stepper CreateStepper(AnimParameter animParameter, Sendable parent)
        {
            return new Stepper(animParameter, parent);
        }

        private static Randomized CreateRandomized(AnimParameter animParameter, Sendable parent)
        {
            return new Randomized(animParameter, parent);
        }
    }
}