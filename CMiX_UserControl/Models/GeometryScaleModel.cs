using CMiX.Services;
using CMiX.ViewModels;
using System;

namespace CMiX.Models
{
    [Serializable]
    public class GeometryScaleModel
    {
        public string MessageAddress { get; set; }

        [OSC]
        public GeometryScaleMode ScaleMode { get; set; }
    }
}
