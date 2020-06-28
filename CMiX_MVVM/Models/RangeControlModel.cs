using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using System;
using CMiX.MVVM.Interfaces;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class RangeControlModel : IModel
    {
        public RangeControlModel()
        {
            Range = new SliderModel();
            Modifier = ((RangeModifier)0).ToString();
        }

        public SliderModel Range { get; set; }
        public string Modifier { get; set; }
        public bool Enabled { get; set; }
    }
}