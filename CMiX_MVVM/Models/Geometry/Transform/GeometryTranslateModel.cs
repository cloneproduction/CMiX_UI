using System;
using CMiX.MVVM.ViewModels;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class GeometryTranslateModel : Model
    {
        public GeometryTranslateModel()
        {

        }

        public GeometryTranslateMode Mode { get; set; }
    }
}