using CMiX.Services;
using CMiX.ViewModels;
using CMiX.MVVM.Models;
using System;

namespace CMiX.Models
{
    [Serializable]
    public class GeometryRotationModel : Model
    {
        public GeometryRotationModel()
        {

        }

        [OSC]
        public GeometryRotationMode Mode { get; set; }

        [OSC]
        public bool RotationX { get; set; }

        [OSC]
        public bool RotationY { get; set; }

        [OSC]
        public bool RotationZ { get; set; }
    }
}
