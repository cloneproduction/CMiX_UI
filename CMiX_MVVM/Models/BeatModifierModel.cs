using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class BeatModifierModel
    {
        public BeatModifierModel()
        {
            ChanceToHit = new SliderModel { Amount = 1.0 };
            Multiplier = 1.0;
        }

        public double Multiplier { get; set; }
        public SliderModel ChanceToHit { get; set; }
    }
}