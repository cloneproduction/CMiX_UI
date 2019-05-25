using CMiX.Services;
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

            ObjColor = "#FF00FF";
            BgColor = "#FF00FF";
        }

        public string MessageAddress { get; set; }

        [OSC]
        public string ObjColor { get; set; }

        [OSC]
        public string BgColor { get; set; }

        public BeatModifierModel BeatModifierModel { get; set; }
        public RangeControlModel HueDTO { get; set; }
        public RangeControlModel SatDTO { get; set; }
        public RangeControlModel ValDTO { get; set; }
    }
}