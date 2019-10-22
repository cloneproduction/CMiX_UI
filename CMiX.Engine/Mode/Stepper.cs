using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.Engine
{
    public class Stepper : Mode
    {
        public Stepper(Stopwatcher stopwatcher)
        {
            Stopwatcher = stopwatcher;
            Stopwatcher.Change += new EventHandler(MovePosition);
            CurrentStepPos = 0.0;
            Step = 4;
            UpdateValue = new Action(Update);
        }

        public int Step { get; set; }

        public double CurrentStepPos { get; set; }

        private void MovePosition(object sender, EventArgs e)
        {
            CurrentStepPos += 1 / (Convert.ToDouble(Step) - 1);
            if (CurrentStepPos > 1.0)
                CurrentStepPos = 0.0;
            ParameterValue = CurrentStepPos;
        }

        public void Update()
        {

        }
    }
}