﻿using System;

namespace CMiX.Models
{
    [Serializable]
    public class GeometryFXModel : Model
    {
        public GeometryFXModel()
        {
            Explode = new SliderModel();
        }

        public SliderModel Explode { get; set; }
    }
}