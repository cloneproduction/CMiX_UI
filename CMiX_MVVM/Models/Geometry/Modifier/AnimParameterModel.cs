﻿using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class AnimParameterModel
    {
        public AnimParameterModel()
        {
            Slider = new SliderModel();
            BeatModifier = new BeatModifierModel();
        }

        public SliderModel Slider { get; set; }
        public BeatModifierModel BeatModifier { get; set; }
    }
}