using CMiX.Services;
using System;

namespace CMiX.Models
{
    [Serializable]
    public class SliderModel
    {
        public SliderModel()
        {

        }

        public SliderModel(string messageaddress)
        {
            MessageAddress = messageaddress;
        }

        public string MessageAddress { get; set; }

        [OSC]
        public double Amount { get; set; }
    }
}