using CMiX.MVVM.Models;
using CMiX.MVVM.Resources;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Mediator;

namespace CMiX.MVVM.ViewModels
{
    public class Stepper : Sender, IAnimMode
    {
        public Stepper(string name, IColleague parentSender) : base (name, parentSender)
        {
            StepCount = 2;
            nextStep = 1.0;
        }

        public override void Receive(Message message)
        {
            this.SetViewModel(message.Obj as StepperModel);
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
                this.Send(new Message(MessageCommand.UPDATE_VIEWMODEL, this.Address, this.GetModel()));
            }
        }

        public void UpdateOnBeatTick(double[] doubleToAnimate, double period, Range range, Easing easing)
        {
            if (nextStep >= StepCount)
                nextStep = 0.0;

            currentStep = nextStep;
            nextStep += 1.0;
        }

        public void UpdateOnGameLoop(double[] doubleToAnimate, double period, Range range, Easing easing)
        {
            for (int i = 0; i < doubleToAnimate.Length; i++)
            {
                doubleToAnimate[i] = Utils.Map(nextStep, 0, StepCount, range.Minimum, range.Maximum);
            }
        }
    }
}