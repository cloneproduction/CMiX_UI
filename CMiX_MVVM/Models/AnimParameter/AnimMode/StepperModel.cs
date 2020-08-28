using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.MVVM.Models
{
    public class StepperModel : IAnimModeModel
    {
        public StepperModel()
        {

        }

        public int StepCount { get; set; }
        public bool Enabled { get; set; }
    }
}
