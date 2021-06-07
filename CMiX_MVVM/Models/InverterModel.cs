using System;

namespace CMiX.MVVM.Models
{
    public class InverterModel : Model
    {
        public InverterModel()
        {
            ID = Guid.NewGuid();
            Invert = new SliderModel();
        }

        public SliderModel Invert { get; set; }
        public string InvertMode { get; set; }
    }
}
