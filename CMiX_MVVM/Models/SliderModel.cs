using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class SliderModel : IModel
    {
        public SliderModel()
        {

        }

        public SliderModel(string messageaddress)
        {
            MessageAddress = messageaddress;
        }

        public double Amount { get; set; }
        public string MessageAddress { get; set; }
    }
}