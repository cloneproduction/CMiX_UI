using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.MVVM.Models.Beat
{
    public class MasterBeatModel : Model
    {
        public MasterBeatModel()
        {

        }

        public double[] Periods { get; set; }
        public double Period { get; set; }
        public double Multiplier { get; set; }
    }
}
