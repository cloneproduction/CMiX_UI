using System;

namespace CMiX.Models
{
    [Serializable]
    public class RangeControlModel
    {
        public RangeControlModel()
        {
            Range = new SliderModel();
        }
        public SliderModel Range { get; set; }
        public string Modifier { get; set; }
    }
}