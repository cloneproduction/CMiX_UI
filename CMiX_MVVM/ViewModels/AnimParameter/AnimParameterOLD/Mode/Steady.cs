using System;

namespace CMiX.Studio.ViewModels
{
    public class Steady : Mode
    {
        public Steady(Stopwatcher stopwatcher)
        {
            Stopwatcher = stopwatcher;
            UpdateValue = new Action(Update);
            ParameterValue = 0.0;
        }

        public void Update()
        {

        }
    }
}
