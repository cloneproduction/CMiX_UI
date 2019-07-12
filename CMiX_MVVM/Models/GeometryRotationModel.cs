//using CMiX.Services;

using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels;
using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class GeometryRotationModel : Model
    {
        public GeometryRotationModel()
        {

        }

        //[OSC]
        public GeometryRotationMode Mode { get; set; }

        //[OSC]
        public bool RotationX { get; set; }

        //[OSC]
        public bool RotationY { get; set; }

        //[OSC]
        public bool RotationZ { get; set; }
    }
}
