using System;
using CMiX.MVVM.ViewModels;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class GeometryScaleModel : IModel
    {
        public GeometryScaleModel()
        {

        }

        public GeometryScaleMode Mode { get; set; }
        public string MessageAddress { get; set; }
    }
}