using System;

namespace CMiX.MVVM.ViewModels
{
    public class Stepper : AnimMode, IAnimMode
    {
        public Stepper(AnimParameter animParameter)
        {
            AnimParameter = animParameter;
        }

        public Stepper(AnimParameter animParameter, Sendable parentSendable) : this(animParameter)
        {
            SubscribeToEvent(parentSendable);
        }

        public double currentStep;
        public double nextStep;

        private int _stepCount;
        public int StepCount
        {
            get => _stepCount;
            set
            {
                SetAndNotify(ref _stepCount, value);
            }
        }

        public override void UpdateOnBeatTick(double period)
        {
            currentStep = nextStep;
            nextStep++;
            if (nextStep >= StepCount)
                nextStep = 0;
        }

        public override double UpdatePeriod(double period)
        {
            return nextStep; // Utils.Map(nextStep, 0, StepCount, Range.Minimum, Range.Maximum);
        }
    }
}