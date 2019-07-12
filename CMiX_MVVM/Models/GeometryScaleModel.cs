//using CMiX.Services;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels;
using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class GeometryScaleModel : Model
    {
        public GeometryScaleModel()
        {

        }

        //[OSC]
        public GeometryScaleMode Mode { get; set; }
    }
}
