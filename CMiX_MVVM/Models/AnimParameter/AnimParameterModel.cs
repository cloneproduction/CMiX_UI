using CMiX.MVVM.ViewModels;
using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class AnimParameterModel : Model
    {
        public AnimParameterModel()
        {
            BeatModifierModel = new BeatModifierModel();
            EasingModel = new EasingModel();
            RangeModel = new RangeModel();
        }

        public string Name { get; set; }
        public bool IsEnabled { get; set; }
        public IAnimMode AnimMode { get; set; }
        public EasingModel EasingModel { get; set; }
        public RangeModel RangeModel { get; set; }
        public BeatModifierModel BeatModifierModel { get; set; }
    }
}