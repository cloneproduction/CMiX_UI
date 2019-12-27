using System;
using CMiX.MVVM.ViewModels;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class GeometryTranslateModel : IModel
    {
        public GeometryTranslateModel()
        {

        }

        public GeometryTranslateMode Mode { get; set; }
        public string MessageAddress { get; set; }
    }
}