using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class ColorationModel
    {
        public ColorationModel()
        {
            BeatModifierModel = new BeatModifierModel();
            ColorSelectorModel = new ColorSelectorModel();
            HueDTO = new RangeControlModel();
            SatDTO = new RangeControlModel();
            ValDTO = new RangeControlModel();
        }

        public ColorSelectorModel ColorSelectorModel { get; set; }
        public BeatModifierModel BeatModifierModel { get; set; }
        public RangeControlModel HueDTO { get; set; }
        public RangeControlModel SatDTO { get; set; }
        public RangeControlModel ValDTO { get; set; }
    }
}