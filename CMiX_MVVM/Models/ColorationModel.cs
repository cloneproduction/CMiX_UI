﻿using CMiX.Core.Models;
using System;

namespace CMiX.Core.Models
{
    [Serializable]
    public class ColorationModel : Model, IModel
    {
        public ColorationModel()
        {
            ID = Guid.NewGuid();
            BeatModifierModel = new BeatModifierModel();
            ColorSelectorModel = new ColorSelectorModel();
            HueModel = new RangeControlModel();
            SatModel = new RangeControlModel();
            ValModel = new RangeControlModel();
        }

        public ColorSelectorModel ColorSelectorModel { get; set; }
        public BeatModifierModel BeatModifierModel { get; set; }
        public RangeControlModel HueModel { get; set; }
        public RangeControlModel SatModel { get; set; }
        public RangeControlModel ValModel { get; set; }
    }
}