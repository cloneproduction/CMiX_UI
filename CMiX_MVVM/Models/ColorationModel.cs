//using CMiX.Services;
using CMiX.MVVM.Models;
using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class ColorationModel : Model
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

        //[OSC]
        public string ObjColor { get; set; }

        //[OSC]
        public string BgColor { get; set; }

        public BeatModifierModel BeatModifierModel { get; set; }
        public RangeControlModel HueDTO { get; set; }
        public RangeControlModel SatDTO { get; set; }
        public RangeControlModel ValDTO { get; set; }
    }
}