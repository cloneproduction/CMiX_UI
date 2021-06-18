using CMiX.MVVM.Interfaces;
using System;

namespace CMiX.MVVM.Models
{
    public class BeatModifierModel : BeatModel, IModel
    {
        public BeatModifierModel()
        {
            this.ID = Guid.NewGuid();
            ChanceToHit = new SliderModel { Amount = 100.0 };
        }

        public int BeatIndex { get; set; }
        public SliderModel ChanceToHit { get; set; }
    }
}