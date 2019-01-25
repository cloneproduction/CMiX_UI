using System;

namespace CMiX.Models
{
    [Serializable]
    public class RangeControlDTO
    {
        public RangeControlDTO()
        {
            Range = new SliderDTO();
        }
        public SliderDTO Range { get; set; }
        public string Modifier { get; set; }
    }
}