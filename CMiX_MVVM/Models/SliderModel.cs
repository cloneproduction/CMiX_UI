using CMiX.MVVM.Interfaces;
using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class SliderModel : Model, IModel
    {
        public SliderModel()
        {
            Amount = 0.0;
            Enabled = true;
        }

        public double Amount { get; set; }
        public string Address { get; set; }
    }
}