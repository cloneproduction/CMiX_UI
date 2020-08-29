using CMiX.MVVM.Resources;
using System;

namespace CMiX.MVVM.ViewModels
{
    public class Stepper : AnimMode
    {
        public Stepper(AnimParameter animParameter)
        {
            AnimParameter = animParameter;
            StepCount = 2;
            nextStep = 1.0;
        }

        public Stepper(AnimParameter animParameter, Sendable parentSendable) : this(animParameter)
        {
            SubscribeToEvent(parentSendable);
        }

        private double currentStep;
        private double nextStep;

        private int _stepCount;
        public int StepCount
        {
            get => _stepCount;
            set
            {
                if (value <= 1)
                    value = 1;
                SetAndNotify(ref _stepCount, value);
                OnSendChange(this.GetModel(), this.GetMessageAddress());
            }
        }

        public override void UpdateOnBeatTick(double period)
        {
            if (nextStep >= StepCount)
                nextStep = 0.0;

            currentStep = nextStep;
            nextStep += 1.0;

        }

        public override double UpdatePeriod(double period)
        {
            return Utils.Map(nextStep, 0, StepCount, AnimParameter.Range.Minimum, AnimParameter.Range.Maximum);
        }
    }
}