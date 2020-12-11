using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.MVVM.Models
{
    public class RangeModel : IRangeModel
    {
        public RangeModel()
        {

        }

        public bool Enabled { get; set; }
        public double Minimum { get; set; }
        public double Maximum { get; set; }
    }
}
