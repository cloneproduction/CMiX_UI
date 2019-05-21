using CMiX.Services;
using System;

namespace CMiX.Models
{
    [Serializable]
    public class BeatModifierModel
    {
        public BeatModifierModel()
        {
            ChanceToHit = new SliderModel();
        }
        public string MessageAddress { get; set; }

        [OSC]
        public double Multiplier { get; set; }

        public SliderModel ChanceToHit { get; set; }
    }
}