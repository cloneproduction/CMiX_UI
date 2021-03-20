using CMiX.MVVM.Interfaces;
using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class BeatModifierModel : IModel
    {
        public BeatModifierModel()
        {
            ChanceToHit = new SliderModel { Amount = 100.0 };
        }

        public double Period { get; set; }
        public int BeatIndex { get; set; }
        public bool Enabled { get; set; }
        public double Multiplier { get; set; }
        public SliderModel ChanceToHit { get; set; }
    }
}