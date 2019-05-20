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

        public SliderModel ChanceToHit { get; set; }
        public double Multiplier { get; set; }
    }
}