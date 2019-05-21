using CMiX.Services;
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
        public string MessageAddress { get; set; }
        public SliderModel Range { get; set; }

        [OSC]
        public string Modifier { get; set; }
    }
}