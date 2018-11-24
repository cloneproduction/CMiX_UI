using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.Models
{
    [Serializable]
    public class LayerFXDTO
    {
        public string MessageAddress { get; set; }

        public double Feedback { get; set; }

        public double Blur { get; set; }
    }
}
