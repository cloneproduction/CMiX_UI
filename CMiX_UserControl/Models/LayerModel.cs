using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.Models
{
    [DataContract]
    [Serializable]
    class LayerModel
    {
        [DataMember]
        public double Fade { get; set; }

        public string BlendMode { get; set; }
    }
}
