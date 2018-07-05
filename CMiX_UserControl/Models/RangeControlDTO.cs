using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.Models
{
    [Serializable]
    public class RangeControlDTO
    {
        public double Range { get; set; }

        public string Modifier { get; set; }
    }
}
