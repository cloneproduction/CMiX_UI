using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.Engine
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