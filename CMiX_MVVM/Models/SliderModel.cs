﻿using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class SliderModel : Model
    {
        public SliderModel()
        {

        }

        public SliderModel(string messageaddress)
        {
            MessageAddress = messageaddress;
        }

        public double Amount { get; set; }
    }
}