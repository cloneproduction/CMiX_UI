using CMiX.MVVM.ViewModels;
using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class AnimParameterModel : Model
    {
        public AnimParameterModel()
        {
            Mode = new AnimModeModel();
            Influence = new SliderModel();
            BeatModifier = new BeatModifierModel();
            RangeModel = new RangeModel();
        }

        public RangeModel RangeModel { get; set; }
        public AnimModeModel Mode { get; set; }
        public SliderModel Influence { get; set; }
        public BeatModifierModel BeatModifier { get; set; }
    }
}