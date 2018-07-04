using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.Models
{
    [Serializable]
    public class BeatModifierDTO
    {
        public double ChanceToHit { get; set; }

        public double Multiplier { get; set; }
    }
}
