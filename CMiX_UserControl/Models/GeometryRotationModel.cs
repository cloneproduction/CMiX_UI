using CMiX.Services;
using CMiX.ViewModels;
using System;

namespace CMiX.Models
{
    [Serializable]
    public class GeometryRotationModel
    {
        public string MessageAddress { get; set; }

        [OSC]
        public GeometryRotationMode RotationMode { get; set; }

        [OSC]
        public bool RotationX { get; set; }

        [OSC]
        public bool RotationY { get; set; }

        [OSC]
        public bool RotationZ { get; set; }
    }
}
