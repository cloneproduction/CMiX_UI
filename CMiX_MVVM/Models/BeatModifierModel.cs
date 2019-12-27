using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class BeatModifierModel : IModel
    {
        public BeatModifierModel()
        {
            ChanceToHit = new SliderModel { Amount = 1.0 };
            Multiplier = 1.0;
        }

        public BeatModifierModel(string messageaddress) 
            : this()
        {
            MessageAddress = String.Format("{0}{1}/", messageaddress, "BeatModifier");
        }

        public double Multiplier { get; set; }
        public SliderModel ChanceToHit { get; set; }
        public string MessageAddress { get; set; }
    }
}