using System;

namespace CMiX.Models
{
    [Serializable]
    public class BeatModifierDTO
    {
        public BeatModifierDTO()
        {
            ChanceToHit = new SliderDTO();
        }

        public SliderDTO ChanceToHit { get; set; }
        public double Multiplier { get; set; }
    }
}