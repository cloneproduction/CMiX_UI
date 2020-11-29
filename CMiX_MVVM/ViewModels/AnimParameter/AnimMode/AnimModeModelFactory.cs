using CMiX.MVVM.Models;

namespace CMiX.MVVM.ViewModels
{
    public static class AnimModeModelFactory
    {
        public static IAnimModeModel GetModel(this IAnimMode instance)
        {
            if (instance is Steady)
                return ((Steady)instance).GetModel();

            else if (instance is LFO)
                return ((LFO)instance).GetModel();

            else if (instance is Randomized)
                return ((Randomized)instance).GetModel();

            else if (instance is Stepper)
                return ((Stepper)instance).GetModel();

            else return null;
        }

        public static void SetViewModel(this IAnimMode instance, IAnimModeModel model)
        {
            if (instance is Steady)
                ((Steady)instance).SetViewModel(model);

            else if (instance is LFO)
                ((LFO)instance).SetViewModel(model);

            else if (instance is Randomized)
                ((Randomized)instance).SetViewModel(model);

            else if (instance is Stepper)
                ((Stepper)instance).SetViewModel(model);
        }



        private static SteadyModel GetModel(this Steady instance)
        {
            SteadyModel model = new SteadyModel();
            model.SteadyType = instance.SteadyType;
            model.LinearType = instance.LinearType;
            model.Seed = instance.Seed;
            return model;
        }

        private static void SetViewModel(this Steady instance, IAnimModeModel animModeModel)
        {
            var model = animModeModel as SteadyModel;
            instance.SteadyType = model.SteadyType;
            instance.LinearType = model.LinearType;
            instance.Seed = model.Seed;
        }



        private static LFOModel GetModel(this LFO instance)
        {
            LFOModel model = new LFOModel();
            model.Invert = instance.Invert;
            return model;
        }

        private static void SetViewModel(this LFO instance, IAnimModeModel animModeModel)
        {
            var model = animModeModel as LFOModel;
            instance.Invert = model.Invert;
        }



        private static RandomizedModel GetModel(this Randomized instance)
        {
            RandomizedModel model = new RandomizedModel();
            return model;
        }

        private static void SetViewModel(this Randomized instance, IAnimModeModel model)
        {

        }



        private static StepperModel GetModel(this Stepper instance)
        {
            StepperModel model = new StepperModel();
            model.StepCount = instance.StepCount;
            return model;
        }

        private static void SetViewModel(this Stepper instance, IAnimModeModel animModeModel)
        {
            StepperModel model = animModeModel as StepperModel;
            instance.StepCount = model.StepCount;
        }
    }
}
