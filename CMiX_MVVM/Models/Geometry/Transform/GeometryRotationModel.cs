using CMiX.MVVM.ViewModels;
using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class GeometryRotationModel : IModel
    {
        public GeometryRotationModel()
        {

        }

        public GeometryRotationMode Mode { get; set; }
        public bool RotationX { get; set; }
        public bool RotationY { get; set; }
        public bool RotationZ { get; set; }
        public string MessageAddress { get; set; }
    }
}
