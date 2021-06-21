using CMiX.Core.Models;
using System;

namespace CMiX.Core.Models
{
    [Serializable]
    public class SliderModel : Model, IModel
    {
        public SliderModel()
        {
            ID = Guid.NewGuid();
            Amount = 0.0;
            Enabled = true;
        }

        public double Amount { get; set; }
        public string Address { get; set; }
    }
}