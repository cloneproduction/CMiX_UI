using CMiX.Core.Presentation.ViewModels;
using System;

namespace CMiX.Core.Models
{
    [Serializable]
    public class GeometryRotationModel
    {
        public GeometryRotationModel()
        {

        }

        public GeometryRotationMode Mode { get; set; }
        public bool RotationX { get; set; }
        public bool RotationY { get; set; }
        public bool RotationZ { get; set; }
    }
}
