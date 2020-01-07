using System;
using CMiX.MVVM.ViewModels;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class GeometryScaleModel
    {
        public GeometryScaleModel()
        {

        }

        public GeometryScaleMode Mode { get; set; }
    }
}