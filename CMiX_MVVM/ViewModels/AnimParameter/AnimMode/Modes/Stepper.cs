using System;
using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public class Stepper : Sendable, IAnimMode 
    {
        public Stepper()
        {
            Easing = new Easing(this);
            StepCount = 1;
        }

        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {

        }

        private int _stepCount;
        public int StepCount
        {
            get => _stepCount;
            set
            {
                if(value > 0)
                    SetAndNotify(ref _stepCount, value);
            }
        }

        public double CurrentStepPos { get; set; }
        public Range Range { get; set; }
        public Easing Easing { get; set; }

        public void Update()
        {
            //Console.WriteLine("Stepper Update");
        }
    }
}