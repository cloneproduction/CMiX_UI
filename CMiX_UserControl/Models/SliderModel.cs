using CMiX.Services;
using System;

namespace CMiX.Models
{
    [Serializable]
    public class SliderModel
    {
        public string MessageAddress { get; set; }

        [OSC]
        public double Amount { get; set; }
    }
}