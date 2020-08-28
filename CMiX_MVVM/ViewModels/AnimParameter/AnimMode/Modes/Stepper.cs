using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using System;

namespace CMiX.MVVM.ViewModels
{
    public class Stepper : AnimMode
    {
        public Stepper(AnimParameter animParameter)
        {
            AnimParameter = animParameter;
        }

        public Stepper(AnimParameter animParameter, Sendable parentSendable) : this(animParameter)
        {
            SubscribeToEvent(parentSendable);
        }

        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {
            if (e.ParentMessageAddress + this.GetMessageAddress() == e.MessageAddress)
                this.SetViewModel(e.Model as SteadyModel);
            else
                OnReceiveChange(e.Model, e.MessageAddress, e.ParentMessageAddress + this.GetMessageAddress());
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

        public AnimParameter AnimParameter { get; set; }

        public void UpdateOnBeatTick(double period)
        {
            currentStep = nextStep;
            nextStep++;
            if (nextStep >= StepCount)
                nextStep = 0;
        }

        public double UpdatePeriod(double period, AnimParameter animParameter)
        {
            return nextStep; // Utils.Map(nextStep, 0, StepCount, Range.Minimum, Range.Maximum);
        }
    }
}