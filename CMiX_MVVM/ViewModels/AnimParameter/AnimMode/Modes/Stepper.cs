using CMiX.MVVM.Resources;
using System;

namespace CMiX.MVVM.ViewModels
{
    public class Stepper : AnimMode
    {
        public Stepper()
        {
            StepCount = 2;
            nextStep = 1.0;
        }

        public Stepper(Sendable parentSendable) : this()
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

        public override void UpdateOnBeatTick(AnimParameter animParameter, double period)
        {
            if (nextStep >= StepCount)
                nextStep = 0.0;

            currentStep = nextStep;
            nextStep += 1.0;
        }

        public override void UpdateParameters(AnimParameter animParameter, double period)
        {
            for (int i = 0; i < animParameter.Parameters.Length; i++)
            {
                animParameter.Parameters[i] = Utils.Map(nextStep, 0, StepCount, animParameter.Range.Minimum, animParameter.Range.Maximum);
            }
        }
    }
}