using CMiX.Services;
using CMiX.ViewModels;
using System;

namespace CMiX.Models
{
    [Serializable]
    public class RangeControlModel : Model
    {
        public RangeControlModel()
        {
            Range = new SliderModel();
            Modifier = ((RangeModifier)0).ToString();
        }

        public SliderModel Range { get; set; }

        [OSC]
        public string Modifier { get; set; }
    }
}