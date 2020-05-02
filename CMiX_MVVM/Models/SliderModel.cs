using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class SliderModel : IModel
    {
        public SliderModel()
        {
            Amount = 0.0;
            Enabled = true;
        }

        public double Amount { get; set; }
        public bool Enabled { get; set; }
    }
}