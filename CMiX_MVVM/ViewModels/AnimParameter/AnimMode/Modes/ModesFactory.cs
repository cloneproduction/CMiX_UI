namespace CMiX.MVVM.ViewModels
{
    public static class ModesFactory
    {
        public static IAnimMode CreateMode(ModeType modeType, Sendable parent)
        {
            IAnimMode animMode = null;

            if (modeType == ModeType.Steady)
                animMode = CreateSteady(parent);
            else if (modeType == ModeType.LFO)
                animMode = CreateLFO(parent);
            else if (modeType == ModeType.Random)
                animMode = CreateRandomized(parent);
            else if (modeType == ModeType.Stepper)
                animMode = CreateStepper(parent);
            else if (modeType == ModeType.None)
                animMode = CreateNone(parent);

            return animMode;
        }

        private static None CreateNone(Sendable parent)
        {
            return new None(parent);
        }

        private static LFO CreateLFO(Sendable parent)
        {
            return new LFO(parent);
        }

        private static Steady CreateSteady(Sendable parent)
        {
            return new Steady(parent);
        }

        private static Stepper CreateStepper(Sendable parent)
        {
            return new Stepper(parent);
        }

        private static Randomized CreateRandomized(Sendable parent)
        {
            return new Randomized(parent);
        }
    }
}