using CMiX.Services;
using CMiX.ViewModels;
using CMiX.MVVM.Models;
using System;

namespace CMiX.Models
{
    [Serializable]
    public class GeometryScaleModel : Model
    {
        public GeometryScaleModel()
        {

        }

        [OSC]
        public GeometryScaleMode Mode { get; set; }
    }
}
