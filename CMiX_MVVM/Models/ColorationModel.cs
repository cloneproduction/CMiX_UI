using CMiX.MVVM.Interfaces;
using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class ColorationModel : Model, IModel
    {
        public ColorationModel()
        {
            BeatModifierModel = new BeatModifierModel();
            ColorSelectorModel = new ColorSelectorModel();
            HueModel = new RangeControlModel();
            SatModel = new RangeControlModel();
            ValModel = new RangeControlModel();
        }

        public ColorSelectorModel ColorSelectorModel { get; set; }
        public BeatModifierModel BeatModifierModel { get; set; }
        public RangeControlModel HueModel { get; set; }
        public RangeControlModel SatModel { get; set; }
        public RangeControlModel ValModel { get; set; }
    }
}