﻿using System;

namespace CMiX.Core.Models
{
    [Serializable]
    public class RotationModifierModel : Model
    {
        public RotationModifierModel()
        {
            Rotation = new AnimParameterModel();
            RotationX = new AnimParameterModel();
            RotationY = new AnimParameterModel();
            RotationZ = new AnimParameterModel();
        }

        public AnimParameterModel Rotation { get; set; }
        public AnimParameterModel RotationX { get; set; }
        public AnimParameterModel RotationY { get; set; }
        public AnimParameterModel RotationZ { get; set; }
    }
}