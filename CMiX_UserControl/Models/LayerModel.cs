using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using CMiX.ViewModels;

namespace CMiX.Models
{
    [DataContract]
    [Serializable]
    class LayerModel
    {
        [DataMember]
        public double Fade { get; set; }

        public string BlendMode { get; set; }

        public double BeatModifierChanceToHit { get; set; }

        public bool ContentEnable { get; set; }

        public double ContentBeatModifierChanceToHit { get; set; }

        public double ContentBeatModifierMultiplier { get; set; }
    }
}
