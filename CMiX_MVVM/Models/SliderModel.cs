using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class SliderModel : IModel
    {
        public SliderModel()
        {

        }

        public double Amount { get; set; }
        public bool Enabled { get; set; }
    }
}