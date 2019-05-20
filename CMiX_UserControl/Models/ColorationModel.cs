using System;

namespace CMiX.Models
{
    [Serializable]
    public class ColorationModel
    {
        public ColorationModel()
        {
            BeatModifierModel = new BeatModifierModel();
            HueDTO = new RangeControlModel();
            SatDTO = new RangeControlModel();
            ValDTO = new RangeControlModel();
        }

        public string MessageAddress { get; set; }
        public string ObjColor { get; set; }
        public string BgColor { get; set; }

        public BeatModifierModel BeatModifierModel { get; set; }
        public RangeControlModel HueDTO { get; set; }
        public RangeControlModel SatDTO { get; set; }
        public RangeControlModel ValDTO { get; set; }
    }
}