using System;

namespace CMiX.Models
{
    [Serializable]
    public class ColorationDTO
    {
        public ColorationDTO()
        {
            BeatModifierDTO = new BeatModifierDTO();
            HueDTO = new RangeControlDTO();
            SatDTO = new RangeControlDTO();
            ValDTO = new RangeControlDTO();
        }
        public string MessageAddress { get; set; }
        public BeatModifierDTO BeatModifierDTO { get; set; }
        public string ObjColor { get; set; }
        public string BgColor { get; set; }
        public RangeControlDTO HueDTO { get; set; }
        public RangeControlDTO SatDTO { get; set; }
        public RangeControlDTO ValDTO { get; set; }
    }
}