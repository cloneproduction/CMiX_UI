using CMiX.MVVM.ViewModels;
using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class AnimParameterModel : Model
    {
        public AnimParameterModel()
        {
            Influence = new SliderModel();
            BeatModifier = new BeatModifierModel();
        }

        public AnimMode Mode { get; set; }
        public SliderModel Influence { get; set; }
        public BeatModifierModel BeatModifier { get; set; }
    }
}