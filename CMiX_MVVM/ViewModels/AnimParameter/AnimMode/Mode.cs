using System;

namespace CMiX.MVVM.ViewModels
{
    public class Mode
    {
        public Mode()
        {

        }

        private double _parameterValue = 0.0;
        public double ParameterValue
        {
            get { return _parameterValue; }
            set { _parameterValue = value; }
        }

        public Stopwatcher Stopwatcher { get; set; }
        public Action UpdateValue { get; set; }
    }
}