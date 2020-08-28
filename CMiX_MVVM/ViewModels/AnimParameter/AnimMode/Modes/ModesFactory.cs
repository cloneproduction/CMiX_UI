namespace CMiX.MVVM.ViewModels
{
    public static class ModesFactory
    {
        public static AnimMode CreateMode(ModeType modeType, AnimParameter animParameter, double defaultValue, Sendable parent)
        {
            AnimMode animMode = null;

            if (modeType == ModeType.Steady)
                animMode = CreateSteady(animParameter, defaultValue, parent);
            else if (modeType == ModeType.LFO)
                animMode = CreateLFO(animParameter, defaultValue, parent);
            else if (modeType == ModeType.Random)
                animMode = CreateRandomized(animParameter, defaultValue, parent);
            else if (modeType == ModeType.Stepper)
                animMode = CreateStepper(animParameter, defaultValue, parent);
            else if (modeType == ModeType.None)
                animMode = CreateNone(animParameter, defaultValue, parent);

            return animMode;
        }

        private static None CreateNone(AnimParameter animParameter, double defaultValue, Sendable parent)
        {
            return new None(parent);
        }

        private static LFO CreateLFO(AnimParameter animParameter, double defaultValue, Sendable parent)
        {
            return new LFO(animParameter, parent);
        }

        private static Steady CreateSteady(AnimParameter animParameter, double defaultValue, Sendable parent)
        {
            return new Steady(animParameter, defaultValue, parent);
        }

        private static Stepper CreateStepper(AnimParameter animParameter, double defaultValue, Sendable parent)
        {
            return new Stepper(animParameter, parent);
        }

        private static Randomized CreateRandomized(AnimParameter animParameter, double defaultValue, Sendable parent)
        {
            return new Randomized(animParameter, parent);
        }
    }
}