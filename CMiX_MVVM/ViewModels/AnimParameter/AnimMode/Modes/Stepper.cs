using System;
using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public class Stepper : Sendable, IAnimMode 
    {
        public Stepper()
        {

        }
        public Stepper(Stopwatcher stopwatcher)
        {
            Easing = new Easing(this);
            CurrentStepPos = 0.0;
            StepCount = 4;
        }

        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {
            //throw new NotImplementedException();
        }

        public int StepCount { get; set; }
        public double CurrentStepPos { get; set; }
        public Range Range { get; set; }
        public Easing Easing { get; set; }

        private void MovePosition(object sender, EventArgs e)
        {
            //CurrentStepPos += 1 / (Convert.ToDouble(StepCount) - 1);
            //if (CurrentStepPos > 1.0)
            //    CurrentStepPos = 0.0;
            //ParameterValue = CurrentStepPos;
        }

        public void Update()
        {
            //Console.WriteLine("Stepper Update");
        }
    }
}