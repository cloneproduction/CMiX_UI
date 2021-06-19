using System;
using CMiX.Core.Presentation.ViewModels;

namespace CMiX.Core.Models
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