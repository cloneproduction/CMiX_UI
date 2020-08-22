using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.MVVM.Models
{
    public class RangeModel : Model
    {
        public RangeModel()
        {

        }

        public double Minimum { get; set; }
        public double Maximum { get; set; }
    }
}
