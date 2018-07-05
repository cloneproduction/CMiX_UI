using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.Models
{
    [Serializable]
    public class PostFXDTO
    {
        public string MessageAddress { get; set; }

        public double Feedback { get; set; }

        public double Blur { get; set; }

        public string Transforms { get; set; }

        public string View { get; set; }
    }
}
