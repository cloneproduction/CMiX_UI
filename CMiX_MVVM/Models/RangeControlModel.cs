//using CMiX.Services;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using System;

namespace CMiX.MVVM.Models
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

        //[OSC]
        public string Modifier { get; set; }
    }
}