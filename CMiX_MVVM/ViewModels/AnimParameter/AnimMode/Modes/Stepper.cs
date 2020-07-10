using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.MVVM.ViewModels
{
    public class Stepper : Mode, IAnimMode 
    {
        public Stepper()
        {

        }
        public Stepper(Stopwatcher stopwatcher)
        {
            Stopwatcher = stopwatcher;
            Stopwatcher.Change += new EventHandler(MovePosition);
            CurrentStepPos = 0.0;
            StepCount = 4;
            UpdateValue = new Action(Update);
        }

        public int StepCount { get; set; }

        public double CurrentStepPos { get; set; }
        public Range Range { get; set; }
        public EasingType EasingType { get; set; }

        private void MovePosition(object sender, EventArgs e)
        {
            CurrentStepPos += 1 / (Convert.ToDouble(StepCount) - 1);
            if (CurrentStepPos > 1.0)
                CurrentStepPos = 0.0;
            ParameterValue = CurrentStepPos;
        }

        public void Update()
        {
            Console.WriteLine("Stepper Update");
        }
    }
}