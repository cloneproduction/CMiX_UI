﻿using CMiX.MVVM.Models;

namespace CMiX.MVVM.ViewModels
{
    public static class AnimModeModelFactory
    {
        public static IAnimModeModel GetModel(this AnimMode instance)
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

        public static void SetViewModel(this AnimMode instance, IAnimModeModel componentModel)
        {
            if (instance is Steady)
                ((Steady)instance).SetViewModel(componentModel);

            else if (instance is LFO)
                ((LFO)instance).SetViewModel(componentModel);

            else if (instance is Randomized)
                ((Randomized)instance).SetViewModel(componentModel);

            else if (instance is Stepper)
                ((Stepper)instance).SetViewModel(componentModel);
        }



        private static SteadyModel GetModel(this Steady instance)
        {
            SteadyModel model = new SteadyModel();
            model.SteadyType = instance.SteadyType;
            return model;
        }

        private static void SetViewModel(this Steady instance, SteadyModel model)
        {
            instance.SteadyType = model.SteadyType;
        }



        private static LFOModel GetModel(this LFO instance)
        {
            LFOModel model = new LFOModel();
            model.Invert = instance.Invert;
            return model;
        }

        private static void SetViewModel(this LFO instance, LFOModel model)
        {
            instance.Invert = model.Invert;
        }



        private static RandomizedModel GetModel(this Randomized instance)
        {
            RandomizedModel model = new RandomizedModel();
            return model;
        }

        private static void SetViewModel(this Randomized instance, RandomizedModel model)
        {

        }



        private static StepperModel GetModel(this Stepper instance)
        {
            StepperModel model = new StepperModel();
            model.StepCount = instance.StepCount;
            return model;
        }

        private static void SetViewModel(this Stepper instance, StepperModel model)
        {
            instance.StepCount = model.StepCount;
        }
    }
}
